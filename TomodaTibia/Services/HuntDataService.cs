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
using System.Reflection;

namespace TomodaTibiaAPI.Services
{
    public interface IHuntDataService
    {
        //Task<PaginatedList<ToDo>> GetList(int? pageNumber, string sortField, string sortOrder);
        Task<Response<SearchResponse>> Search(string charName);
        Task<Response<HuntResponse>> HuntDetail(int idHunt);
        Task<Response<HuntRequest>> GetHuntToUpdate(int idHunt, int idAuthor);
        Task<Response<string>> Add(HuntRequest huntReq, int idAuthor);
        Task<Response<string>> Update(HuntRequest huntReq, int idAuthor);
        Task<Response<string>> Remove(int idHunt, int idAuthor);
        Task<Response<List<HuntResponse>>> AuthorHuntList(int idAuthor);
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
            var response = new Response<SearchResponse>(new SearchResponse());
            var responseCharacter = new Response<CharacterResponse>(new CharacterResponse());

            try
            {
                responseCharacter = await Character(characterName);
                response.Data.Character = responseCharacter.Data;

                try
                {
                    //Identifica o ID da vocacao do Player consultante.
                    var idVocation = _db.Vocations
                        .Where(v => v.Name == response.Data.Character.vocacao)
                        .Select(v => v.Id)
                        .FirstAsync();

                    //Identifica os ids das hunts validas para o player consultante
                    var huntsValidas = _db.Players
                        .Where(p => p.Vocation == idVocation.Result)
                        .Select(p => p.Id)
                        .Distinct();

                    //Filtra as hunts por nível e vocação e gera uma coleção de cards
                    var hunts = _db.Hunts
                        .Where(h =>
                        huntsValidas.Contains(h.Id)
                        && response.Data.Character.level >= h.NivelMinReq
                        && h.IsValid == true)

                        .Select(h => new HuntCardResponse
                        {
                            Id = h.Id,
                            Nome = h.Name,
                            NivelMinRec = h.NivelMinReq,
                            Monsters = _db.HuntMonsters
                                .Where(m => m.IdHunt == h.Id)
                                .Select(m => m.IdMonsterNavigation.Img).ToList()

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
        public async Task<Response<HuntResponse>> HuntDetail(int idHunt)
        {
            var response = new Response<HuntResponse>(new HuntResponse());

            try
            {

                var hunt = _mapper.Map<HuntResponse>(await _db.Hunts
                    .Where(h => h.Id == idHunt && h.IsValid == true).FirstOrDefaultAsync());

                if (hunt != null)
                {
                    hunt.Players = await _db.Players
                        .Where(h => h.IdHunt == idHunt)
                        .Include(e => e.Equipaments)
                        .Select(p => new PlayerResponse()
                        {
                            Vocation = _db.Vocations.Where(v => v.Id == p.Vocation).Select(v => v.Name).First(),
                            Level = p.Level,
                            Equipaments = _mapper.Map<EquipamentResponse>(p.Equipaments.First())
                        })
                        .ToListAsync();


                    hunt.OtherItems = await _db.HuntItems
                        .Where(i => i.IdHunt == idHunt)
                        .Select(i => new ItemResponse
                        {
                            Img = i.IdItemNavigation.Img,
                            Qty = i.Qty

                        }).ToListAsync();

                    //Preys desta hunt.
                    hunt.Preys = await _db.HuntPreys
                        .Where(h => h.IdHunt == idHunt)
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
                    hunt.Versions = await _db.HuntClientVersions
                        .Where(h => h.IdHunt == idHunt)
                        .Select(v => v.IdClientVersionNavigation.VersionName)
                        .ToListAsync();

                    hunt.HuntMonsters = await _db.HuntMonsters
                        .Where(m => m.IdHunt == idHunt)
                        .Select(mo => new MonsterResponse()
                        {
                            Img = mo.IdMonsterNavigation.Img
                        })
                        .ToListAsync();


                    response.Data = hunt;
                    response.Message = "Search completed successfully.";
                }
                else
                {
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Hunt not found.";
                }
            }
            catch
            {
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Error when searching.";
            }

            return response;
        }

        public async Task<Response<string>> Add(HuntRequest huntReq, int idAuthor)
        {
            var response = new Response<string>(string.Empty);

            var newHunt = _mapper.Map<Hunt>(huntReq);
            newHunt.IdAuthor = idAuthor;

            newHunt.IsValid = false;

            try
            {
                _db.Hunts.Add(newHunt);
                await _db.SaveChangesAsync();
                response.Message = "Hunt added.";
            }
            catch
            {
                response.Message = "Error when saving hunt." + huntReq.HuntItems.First().IdItem;
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        //Atualizada uma hunt.
        public async Task<Response<string>> Update(HuntRequest huntReq, int idAuthor)
        {
            var response = new Response<string>(string.Empty);

            var updatedHunt = _mapper.Map<Hunt>(huntReq);
            updatedHunt.IdAuthor = idAuthor;

            try
            {
                var huntInDb = await _db.Hunts
                    .SingleOrDefaultAsync(h =>
                    h.Id == updatedHunt.Id
                    && h.IdAuthor == updatedHunt.IdAuthor
                    && h.IsValid == true);

                if (huntInDb != null)
                {
                    updatedHunt.IsValid = false;

                    _db.Hunts.Remove(huntInDb);
                    _db.Hunts.Add(CleanHuntKeysToUpdate(updatedHunt));
                    _db.SaveChanges();

                    response.Data = updatedHunt.Id.ToString();
                    response.Message = "The hunt was updated.";
                }
                else
                {

                    response.Message = "You are not the author of this hunt or it does not exist.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.SetErrors(ex);
                response.Message = "Error updating the hunt.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        //Obtem uma hunt para ser atualizada.
        public async Task<Response<HuntRequest>> GetHuntToUpdate(int idHunt, int idAuthor)
        {

            var response = new Response<HuntRequest>(new HuntRequest());

            try
            {
                var partialHunt = await _db.Hunts.FirstOrDefaultAsync(h =>
                h.Id == idHunt
                && h.IdAuthor == idAuthor
                && h.IsValid == true);

                if (partialHunt != null)
                {
                    var hunt = await _db.Hunts.Where(h => h.Id == idHunt && h.IdAuthor == idAuthor)
                        .Include(h => h.HuntClientVersions)
                        .Include(h => h.HuntImbuements)
                        .Include(h => h.HuntItems)
                        .Include(h => h.HuntMonsters)
                        .Include(h => h.HuntPreys)
                        .Include(h => h.Players)
                        .ThenInclude(h => h.Equipaments)
                        .FirstAsync();

                    response.Data = _mapper.Map<HuntRequest>(hunt);

                    response.Message = "Hunt Found.";
                }
                else
                {
                    response.Message = "You are not the author of this hunt or it does not exist.";
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            catch
            {
                response.Message = "Error searching the hunt.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
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
                    //Removido logicamente.
                    huntToRemove.IsValid = false;
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
            else
            {
                response.Message = "You are not the author of this hunt or it does not exist.";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status400BadRequest;
            }

            return response;
        }

        //Lista as hunts do então author.
        public async Task<Response<List<HuntResponse>>> AuthorHuntList(int idAuthor)
        {
            var response = new Response<List<HuntResponse>>(new List<HuntResponse>());

            try
            {
                response.Data = _mapper
                    .Map<List<HuntResponse>>(await _db.Hunts
                    .Where(h => h.IdAuthor == idAuthor && h.IsValid == true)
                    .ToListAsync());

                if (response.Data.Count != 0)
                {
                    response.Message = $"{response.Data.Count} Hunt(s) found!.";
                }
                else
                {
                    response.Message = "No hunt was found.";
                    response.Data = null;
                }
            }
            catch (Exception ex)
            {
                response.SetErrors(ex);
                response.Message = "Error when consulting your hunts";
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }

        //Consulta as informações do personagem atraves da API do tibia.
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

        //Limpa campos de primary keys para inserir nova entidade.
        private Hunt CleanHuntKeysToUpdate(Hunt hunt)
        {
            hunt.Id = 0;

            foreach (var verions in hunt.HuntClientVersions)
            {
                verions.Id = 0;
            }

            foreach (var player in hunt.Players)
            {
                player.Id = 0;
                player.Equipaments.First().Id = 0;
            }

            foreach (var imbue in hunt.HuntImbuements)
            {
                imbue.Id = 0;
            }

            foreach (var item in hunt.HuntItems)
            {
                item.Id = 0;
            }

            foreach (var monster in hunt.HuntMonsters)
            {
                monster.Id = 0;
            }

            foreach (var prey in hunt.HuntPreys)
            {
                prey.Id = 0;
            }

            return hunt;

        }
    }
}
