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
using TomodaTibiaAPI.Utils.Pagination;

using TomodaTibiaAPI.BLL;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaAPI.Services
{
    public interface IHuntDataService
    {
        //Task<PaginatedList<ToDo>> GetList(int? pageNumber, string sortField, string sortOrder);
        Task<Response<SearchResponse>> Search(SearchParameterRequest parameters);
        Task<Response<HuntResponse>> HuntDetail(int idHunt);
        Task<Response<HuntRequest>> HuntToUpdate(int idHunt, int idAuthor);
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
        private readonly HuntBLL _bll;
        private List<string> Errors;

        public HuntDataService(TomodaTibiaContext db, TibiaApiService api, IMapper mapper, HuntBLL huntbll)
        {
            _db = db;
            _api = api;
            _mapper = mapper;
            _bll = huntbll;
            Errors = new List<string>();
        }

        //Consulta as hunts ideias para o player.
        public async Task<Response<SearchResponse>> Search(SearchParameterRequest parameters)
        {
            var response = new Response<SearchResponse>(new SearchResponse());
            var responseCharacter = new Response<CharacterResponse>(new CharacterResponse());

            //Valida parametros.
            var checkParameters = _bll.CheckParameters(parameters);
            if (checkParameters.Succeeded)
            {
                try
                {
                    //é uma pesquisa por nome?
                    if (!string.IsNullOrEmpty(parameters.CharacterName))
                    {
                        responseCharacter = await GetAPICharacter(parameters.CharacterName);
                        response.Data.Character = responseCharacter.Data;

                        //Identifica o ID da vocacao do Player.
                        var idVocation = await _db.Vocations
                            .Where(v => v.Name == response.Data.Character.Vocation)
                            .Select(v => v.Id)
                            .FirstAsync();

                        //Copia os valores do character para filtragem
                        parameters.ConfiureDefaults();
                        parameters.Vocation.Add(idVocation);
                        parameters.Level.Add(response.Data.Character.level);
                        parameters.IsPremium = response.Data.Character.IsPremium;
                    }

                    try
                    {
                        //Identifica os ids das hunts validas para o player consultante
                        var huntsValidas = await _db.Players
                            .Where(p => parameters.Vocation.Contains(p.Vocation))
                            .Select(p => p.IdHunt)
                            .Distinct()
                            .ToListAsync();

                        //Filtra as hunts com base nos parametros fornecidos.
                        var hunts = await _db.Hunts
                            .Where(h =>
                            huntsValidas.Contains(h.Id)
                            && (parameters.QtyPlayer.Contains(h.QtyPlayer))
                            && parameters.IdClientVersion.Contains(h.HuntClientVersions.First().IdClientVersion)
                            && h.IsValid == true
                            && h.IsPremium == parameters.IsPremium
                            && h.XpHr >= parameters.XpHr
                            && h.Rating >= parameters.Rating
                            && parameters.IdAuthor == 0 || h.IdAuthor == parameters.IdAuthor
                            && (h.LevelMinReq > parameters.Level.First() && h.LevelMinReq <= parameters.Level.Last())
                            && (h.Difficulty >= parameters.Difficulty.First() && h.Difficulty <= parameters.Difficulty.Last())
                            && parameters.IdLoot == 0 || h.HuntLoots.First().IdItem == parameters.IdLoot)
                            .Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize)
                            .Select(h => new HuntCardResponse
                            {
                                Id = h.Id,
                                Nome = h.Name,
                                LevelMinReq = h.LevelMinReq,
                                Monsters = _db.HuntMonsters
                                    .Where(m => m.IdHunt == h.Id)
                                    .Select(m => m.IdMonsterNavigation.Img).ToList()

                            }).ToListAsync();

                        string mens = string.Format("{0}",
                            hunts.Count == 0 ? "No hunt matched your query, change your query parameters." : ""); ;

                        if (hunts.Count != 0)
                        {
                            response.Data.Hunts = hunts;
                            response.Sucess(mens, response.Data);
                        }
                        else
                        {
                            Errors.Add(mens);
                            response.Failed(Errors, StatusCodes.Status400BadRequest);
                        }
                    }
                    catch
                    {
                        Errors.Add("Error searching for hunts.");
                        response.Failed(Errors, StatusCodes.Status500InternalServerError);
                    }
                }
                catch
                {
                    //Erros setados dentro do metodo Character(string characterName) 
                    response.Failed(responseCharacter.Errors, responseCharacter.StatusCode);
                }
            }
            else
            {
                response.Failed(checkParameters.Errors, checkParameters.StatusCode);
            }

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

                    hunt.Loot = await _db.HuntLoots
                        .Where(l => l.IdHunt == idHunt)
                        .Select(loot => new LootResponse()
                        {
                            Img = loot.IdItemNavigation.Img
                        }).ToListAsync();

                    response.Sucess("Search completed successfully.", hunt);
                }
                else
                {
                    Errors.Add("Hunt not found.");
                    response.Failed(Errors, StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                Errors.Add("Error when searching.");
                response.Failed(Errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        //Adiciona um hunt.
        public async Task<Response<string>> Add(HuntRequest huntReq, int idAuthor)
        {
            var response = new Response<string>(string.Empty);
            var checkHunt = _bll.CheckHuntReq(huntReq);

            if (checkHunt.Succeeded)
            {
                try
                {
                    var huntQueueLimit = _db.Hunts.Where(h => h.IsValid == false && h.IdAuthor == idAuthor).Count();

                    if (huntQueueLimit < 25)
                    {
                        var newHunt = _mapper.Map<Hunt>(huntReq);
                        newHunt.IdAuthor = idAuthor;

                        newHunt.IsValid = false;
                        _db.Hunts.Add(newHunt);
                        await _db.SaveChangesAsync();

                        response.Sucess("Hunt added.", newHunt.Id.ToString());
                    }
                    else
                    {
                        Errors.Add("Approval limit exceeded, the maximum is 25.");
                        response.Failed(Errors, StatusCodes.Status400BadRequest);
                    }

                }
                catch
                {
                    Errors.Add("Error when saving hunt.");
                    response.Failed(Errors, StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                response.Failed(checkHunt.Errors, checkHunt.StatusCode);
            }

            return response;
        }

        //Atualizada uma hunt.
        public async Task<Response<string>> Update(HuntRequest huntReq, int idAuthor)
        {
            var response = new Response<string>(string.Empty);
            var checkHunt = _bll.CheckHuntReq(huntReq);

            if (checkHunt.Succeeded)
            {
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

                        response.Sucess("The hunt was updated.", updatedHunt.Id.ToString());
                    }
                    else
                    {
                        Errors.Add("You are not the author of this hunt or it does not exist.");
                        response.Failed(Errors, StatusCodes.Status400BadRequest);
                    }
                }
                catch
                {
                    Errors.Add("Error updating the hunt.");
                    response.Failed(Errors, StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                response.Failed(checkHunt.Errors, checkHunt.StatusCode);
            }

            return response;
        }

        //Remove uma hunt.
        public async Task<Response<string>> Remove(int idHunt, int idAuthor)
        {
            var response = new Response<string>(string.Empty);

            var huntToRemove = await _db.Hunts.SingleOrDefaultAsync(h => h.Id == idHunt && h.IdAuthor == idAuthor && h.IsValid == true);

            if (huntToRemove != null)
            {
                try
                {
                    //Removido logicamente.
                    huntToRemove.IsValid = false;
                    huntToRemove.DescHunt += huntToRemove.Name;
                    huntToRemove.Name = "(REMOVED):" + huntToRemove.Name.Length;
                    _db.SaveChanges();

                    response.Sucess("The hunt was removed.", string.Empty);
                }
                catch
                {
                    Errors.Add("Error removing the hunt.");
                    response.Failed(Errors, StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                Errors.Add("You are not the author of this hunt or it does not exist.");
                response.Failed(Errors, StatusCodes.Status400BadRequest);
            }

            return response;
        }

        //Obtem uma hunt para ser atualizada.
        public async Task<Response<HuntRequest>> HuntToUpdate(int idHunt, int idAuthor)
        {
            var response = new Response<HuntRequest>(new HuntRequest());

            try
            {
                var partialHunt = await _db.Hunts.FirstOrDefaultAsync(h =>
                h.Id == idHunt
                && h.IdAuthor == idAuthor);

                if (partialHunt != null)
                {
                    var hunt = await _db.Hunts.Where(h => h.Id == idHunt && h.IdAuthor == idAuthor)
                        .Include(h => h.HuntClientVersions)
                        .Include(h => h.HuntImbuements)
                        .Include(h => h.HuntItems)
                        .Include(h => h.HuntMonsters)
                        .Include(h => h.HuntPreys)
                        .Include(h => h.HuntLoots)
                        .Include(h => h.Players)
                        .ThenInclude(h => h.Equipaments)
                        .FirstAsync();

                    response.Sucess("Hunt Found.", _mapper.Map<HuntRequest>(hunt));
                }
                else
                {
                    Errors.Add("You are not the author of this hunt or it does not exist.");
                    response.Failed(Errors, StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                Errors.Add("Error searching the hunt.");
                response.Failed(Errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        //Lista as hunts do então author.
        public async Task<Response<List<HuntResponse>>> AuthorHuntList(int idAuthor)
        {
            var response = new Response<List<HuntResponse>>(new List<HuntResponse>());

            try
            {
                var hunts = _mapper
                    .Map<List<HuntResponse>>(await _db.Hunts
                    .Where(h => h.IdAuthor == idAuthor)
                    .ToListAsync());

                if (response.Data.Count != 0)
                {
                    response.Sucess($"{response.Data.Count} Hunt(s) found!.", hunts);
                }
                else
                {
                    response.Sucess("No hunt was found.", null);
                }
            }
            catch
            {
                Errors.Add("Error when consulting your hunts");
                response.Failed(Errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        //Consulta as informações do personagem atraves da API do tibia.
        private async Task<Response<CharacterResponse>> GetAPICharacter(string characterName)
        {
            var response = new Response<CharacterResponse>(null);
            var isValidName = _bll.CheckCharName(characterName);

            if (isValidName.Succeeded)
            {
                try
                {
                    var characterSearch = await _api.Character(characterName);

                    if (characterSearch.characters.error != null)
                    {
                        Errors.Add($"Character {characterName} does not exist.");
                        response.Failed(Errors, StatusCodes.Status400BadRequest);

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
                    Errors.Add("Error conecting to Tibia APi.");
                    response.Failed(Errors, StatusCodes.Status503ServiceUnavailable);
                }
            }
            else
            {
                response.Failed(isValidName.Errors, isValidName.StatusCode);
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

            foreach (var loot in hunt.HuntLoots)
            {
                loot.Id = 0;
            }

            return hunt;

        }

    }
}
