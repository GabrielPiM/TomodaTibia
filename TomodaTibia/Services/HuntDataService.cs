using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TomodaTibiaModels.DB.Response;
using Microsoft.EntityFrameworkCore;

using EFDataAcessLibrary.Models;
using TomodaTibiaModels.Character.Response;
using TomodaTibiaModels.Hunt.Response;
using TomodaTibiaModels.DB.Request;
using AutoMapper;
using TomodaTibiaAPI.Utils;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace TomodaTibiaAPI.Services
{
    public interface IHuntDataService
    {

        //Task<PaginatedList<ToDo>> GetList(int? pageNumber, string sortField, string sortOrder);
        Task<Response<SearchResponse>> Search(string charName);
        Task<Response<HuntResponse>> Detail(int idHunt);
        Task<Response<string>> Add(HuntRequest huntReq);
        Task<Response<string>> Update(HuntRequest huntReq, int idAuthor);
        Task<Response<string>> Remove(int idHunt, int idAuthor);

    }
    public class HuntDataService : IHuntDataService
    {
        private readonly TomodaTibiaContext _db;
        private readonly TibiaApiService _api;
        private readonly IMapper _mapper;

        public HuntDataService(TomodaTibiaContext db, TibiaApiService api, IMapper mapper)
        {
            _db = db;
            _api = api;
            _mapper = mapper;
        }

        //Consulta as hunts ideias para o player.
        public async Task<Response<SearchResponse>> Search(string characterName)
        {
            var response = new Response<SearchResponse>(null);
            var responseCharacter = new Response<CharacterResponse>(null);

            try
            {
                responseCharacter = await Character(characterName);
                response.Data.Character = responseCharacter.Data;

                try
                {
                    //Identifica o ID da vocacao do Player
                    var idVoc = _db.Vocations
                        .Where(v => v.Name == response.Data.Character.vocacao)
                        .Select(v => v.Id)
                        .FirstAsync();

                    //Identifica os ids das hunts validas
                    var huntsValidas = _db.Players
                        .Where(p => p.Id == idVoc.Result)
                        .Select(p => p.Id)
                        .Distinct();

                    //Filtra as hunts por nível e vocação e gera uma coleção de cards
                    var hunts = _db.Hunts
                        .Where(h => huntsValidas
                        .Contains(h.Id) && response.Data.Character.level >= h.NivelMinReq)
                        .Select(h => new HuntCardResponse
                        {
                            Id = h.Id,
                            Nome = h.Name,
                            NivelMinRec = h.NivelMinReq,
                            URLGif = _db.Monsters
                                .Where(m => m.Id == h.Id)
                                .Select(m => m.Img)

                        }).ToListAsync();
                    
                    response.Data.Hunts = hunts.Result;
                }
                catch
                {
                    response.Message = "Error searching for hunts.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            catch 
            {
                //Erros setados dentro do metodo Character(string characterName) 
                response.Message = responseCharacter.Message;
                response.Succeeded = responseCharacter.Succeeded;
                response.StatusCode = responseCharacter.StatusCode;                 
            }

            //Retorna as hunts ou os erros ocorridos.
            return response;
        }

        //Consulta as informações detalhadas de uma hunt.
        public async Task<Response<HuntResponse>> Detail(int idHunt)
        {
            var response = new Response<HuntResponse>(null);

            try
            {
                //Consulta informacoes da Hunt 
                var hunt = await _db.Hunts
                    .Where(h => h.Id == idHunt)
                    .Select(h => new HuntResponse
                    {
                        Name = h.Name,
                        NivelMinReq = h.NivelMinReq,
                        XpHr = h.XpHr,
                        QtyPlayer = h.QtyPlayer,
                        VideoTutorialUrl = h.VideoTutorialUrl,
                        DescHunt = h.DescHunt,
                        Difficulty = h.Difficulty,
                        Rating = h.Rating,
                        IsPremium = h.IsPremium

                    }).FirstAsync();

                //Return caso não encontrada
                if (hunt == null) return null;

                hunt.Players = await _db.Players.Where(p => p.IdHunt == idHunt)
                    .Select(p => new PlayerResponse
                    {
                        Vocation = p.Vocation,
                        Level = p.Level,
                        Equipaments = _db.Equipaments.Where(e => e.Id == p.Id)
                        .Select(e => new EquipamentResponse
                        {
                            Amulet = e.Amulet,
                            Bag = e.Bag,
                            Helmet = e.Helmet,
                            Armor = e.Armor,
                            WeaponRight = e.WeaponRight,
                            WeaponLeft = e.WeaponLeft,
                            Ring = e.Ring,
                            Legs = e.Legs,
                            Boots = e.Boots,
                            Ammo = e.Ammo

                        }).First()

                    }).ToListAsync();

                //Consulta outros Items da hunt
                hunt.OtherItems = await _db.HuntItems
                    .Where(i => i.IdHunt == idHunt)
                    .Select(i => new ItemResponse
                    {
                        Img = i.IdItemNavigation.Img,
                        Qty = i.Qty

                    }).ToListAsync();

                //Preys desta hunt.
                hunt.Preys = await _db.HuntPreys.Where(h => h.IdHunt == idHunt)
                    .Select(p => new PreyResponse
                    {
                        Img = p.IdMonsterNavigation.Img,
                        ReccStars = p.ReccStar,
                        Monster = new MonsterResponse
                        {
                            Img = p.IdMonsterNavigation.Img
                        }
                    }).ToListAsync();

                //Imbuements desta hunt.
                hunt.Imbuements = await _db.HuntImbuements.Where(hi => hi.IdHunt == idHunt)
                    .Select(h => new ImbuementResponse
                    {
                        Category = h.IdImbuementNavigation.Category,

                        Value = _db.ImbuementDescs
                        .Where(desc => desc.IdImbuementLevel == h.IdImbuementLevel
                            && desc.IdImbuementType == h.IdImbuementType)
                        .Select(desc2 => desc2.Value)
                        .First(),

                        Desc = h.IdImbuementNavigation.Desc,
                        Level = h.IdImbuementLevelNavigation.Name,
                        Qty = h.Qty,
                        Img = h.IdImbuementNavigation.Img,

                        Items = _db.ImbuementItems
                        .Where(i => i.IdImbuement == h.IdImbuementNavigation.Id
                          && i.IdImbuementLevel <= h.IdImbuementLevel)
                        .Select(it => new ItemResponse
                        {
                            Img = it.IdItemNavigation.Img,
                            Qty = it.Qty
                        }).ToList()

                    }).ToListAsync();


                //Versões do client dessa hunt
                hunt.Versions = await _db.HuntClientVersions.Where(h => h.IdHunt == idHunt)
                    .Select(cv => cv.IdClientVersionNavigation.VersionName)
                    .ToListAsync();

                response.Data = hunt;
                response.Message = "Search completed successfully.";
            }
            catch(Exception ex)
            {
                response.Errors = new string[3];

                response.Errors[0] = ex.Message;
                response.Errors[1] = ex.StackTrace;
           
         

                response.Message = "Error when searching.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }


            //Retorna o HuntDetail
            return response;
        }

        public async Task<Response<string>> Add(HuntRequest huntReq)
        {
            var response = new Response<string>(string.Empty);

            var newHunt = _mapper.Map<Hunt>(huntReq);
            try
            {
                _db.Hunts.Add(newHunt);
                await _db.SaveChangesAsync();
                response.Message = "Hunt added.";
            }
            catch
            {
                response.Message = "Error when saving hunt.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        public async Task<Response<string>> Update(HuntRequest huntReq, int idAuthor)
        {
            var response = new Response<string>(string.Empty);

            var huntToUpdate = _mapper.Map<Hunt>(huntReq);
            huntToUpdate.IdAuthor = idAuthor;

            var existsOrOwner = await _db.Hunts.SingleOrDefaultAsync(h => h.Id == huntToUpdate.Id && h.IdAuthor == huntToUpdate.IdAuthor);

            if (existsOrOwner != null)
            {
                try
                {
                    _db.Hunts.Update(huntToUpdate);
                    _db.SaveChanges();
                    response.Message = "The hunt was removed.";
                }
                catch
                {
                    response.Message = "Error removing the hunt.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            {
                response.Message = "You are not the author of this hunt or it does not exist.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        public async Task<Response<string>> Remove(int idHunt, int idAuthor)
        {
            var response = new Response<string>(string.Empty);

            var huntToRemove = await _db.Hunts.SingleOrDefaultAsync(h => h.Id == idHunt && h.IdAuthor == idAuthor);

            if (huntToRemove != null)
            {
                try
                {
                    _db.Hunts.Remove(huntToRemove);
                    _db.SaveChanges();
                    response.Message = "The hunt was removed.";
                }
                catch
                {
                    response.Message = "Error removing the hunt.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }
            }
            {
                response.Message = "You are not the author of this hunt or it does not exist.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        //Consulta as informações do personagem.
        private async Task<Response<CharacterResponse>> Character(string characterName)
        {
            var response = new Response<CharacterResponse>(null);

            try
            {
                var characterSearch = await _api.Character(characterName);

                if (characterSearch.characters.error != null)
                {
                    response.Message = $"Character {characterName} does not exist.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                  response.Data = new CharacterResponse
                    (
                      characterSearch.characters.data.name,
                      characterSearch.characters.data.level,
                      characterSearch.characters.data.vocation,
                      characterSearch.characters.data.sex,
                      characterSearch.characters.data.account_status
                    );
                }
            }
            catch
            {
                response.Message = "Error conecting to Tibia APi.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            }

            return response;
        }
    }
}
