using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TomodaTibiaModels.DB.Response;
using Microsoft.EntityFrameworkCore;

using TomodaTibiaAPI.EntityFramework;
using TomodaTibiaModels.Character.Response;
using TomodaTibiaModels.Hunt.Response.Search;
using TomodaTibiaModels.DB.Request;
using AutoMapper;
using TomodaTibiaAPI.Utils;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Reflection;
using TomodaTibiaAPI.Utils.Pagination;

using TomodaTibiaAPI.BLL;
using TomodaTibiaModels.Utils;
using TomodaTibiaModels.Utils.DBMaps;

namespace TomodaTibiaAPI.Services
{
    public interface IHuntDataService
    {

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

                        List<int> IdValidMonsters = new List<int>();

                        //loot filter
                        if (parameters.IdLoot != 0)
                        {
                            IdValidMonsters = await _db.Monsters.Where(m => m.MonsterLoots
                            .All(mp => mp.IdItem == parameters.IdLoot))
                            .Select(ms => ms.Id).ToListAsync();

                        }


                        //Filtra as hunts com base nos parametros fornecidos.
                        var hunts = await _db.Hunts
                            .Where(h =>
                            huntsValidas.Contains(h.Id)
                            && parameters.QtyPlayer.First() == 0 || parameters.QtyPlayer.Contains(h.TeamSize)
                            && parameters.IdClientVersion.Contains(h.HuntClientVersions.First().IdClientVersion)
                            && h.IdSituation == HuntSituationMap.Accepted
                            && h.IsPremium == parameters.IsPremium
                            && h.XpHr >= parameters.XpHr
                            && h.Rating >= parameters.Rating
                            && parameters.IdAuthor == 0 || h.IdAuthor == parameters.IdAuthor
                            && (h.LevelMinReq > parameters.Level.First() && h.LevelMinReq <= parameters.Level.Last())
                            && (h.Difficulty >= parameters.Difficulty.First() && h.Difficulty <= parameters.Difficulty.Last())
                            && (parameters.IdLoot == 0 || h.HuntMonsters.All(m => IdValidMonsters.Contains(m.Id))))
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
                            hunts.Count == 0 ? "No hunt matched your query, change your query parameters." : "");

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
                    .Where(h => h.Id == idHunt && h.IdSituation == HuntSituationMap.Accepted).FirstOrDefaultAsync());

                if (hunt != null)
                {
                    hunt.Players = await _db.Players
                        .Where(h => h.IdHunt == idHunt)
                        .Include(pe => pe.Equipaments)
                        .Select(p => new PlayerResponse()
                        {
                            Vocation = _db.Vocations.Where(wv => wv.Id == p.Vocation).Select(sv => sv.Name).FirstOrDefault(),

                            Function = p.Function,

                            Level = p.Level,

                            Equipments = _mapper.Map<EquipamentResponse>(p.Equipaments.FirstOrDefault()),

                            Items = _db.PlayerItems.Where(wpi => wpi.IdPlayer == p.Id).Select(spi => new ImgObjResponse()
                            {
                                Img = spi.IdItemNavigation.Img

                            }).ToList(),

                            Imbuements = _db.PlayerImbuements.Where(wpi => wpi.Id == p.Id).Select(spi => new ImbuementResponse()
                            {
                                Category = spi.IdImbuementNavigation.Category,

                                Img = spi.IdImbuementNavigation.Img,

                                Desc = _db.ImbuementValues.Where(wiv => wiv.IdImbuementType == spi.IdImbuementType
                                        && wiv.IdImbuementLevel == spi.IdImbuementLevel).Select(siv => siv.Value)
                                        + spi.IdImbuementNavigation.Desc,

                                Level = spi.IdImbuementLevelNavigation.Name,

                                Qty = spi.Qty,

                                Items = _db.ImbuementItems.Where(wii => wii.Id == spi.Id).Select(sii => new ImbuementItemResponse()
                                {
                                    Img = sii.IdItemNavigation.Img,
                                    Qty = sii.Qty

                                }).ToList(),

                            }).ToList(),

                            Preys = _db.PlayerPreys.Where(wpp => wpp.IdPlayer == p.Id).Select(spp => new PreyResponse()
                            {
                                TypeImg = spp.IdPreyNavigation.Img,
                                MonsterImg = spp.IdMonsterNavigation.Img,
                                Stars = spp.ReccStar

                            }).ToList()

                        })
                        .ToListAsync();


                    //Versões do client dessa hunt
                    hunt.HuntClientVersions = await _db.HuntClientVersions
                        .Where(h => h.IdHunt == idHunt)
                        .Select(v => v.IdClientVersionNavigation.VersionName)
                        .ToListAsync();

                    hunt.HuntMonsters = await _db.HuntMonsters
                        .Where(m => m.IdHunt == idHunt)
                        .Select(mo => new HuntMonsterResponse()
                        {
                            Img = mo.IdMonsterNavigation.Img,
                            Qty = mo.Qty

                        })
                        .ToListAsync();



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

            huntReq.LevelMinReq = huntReq.Players.ToArray().Select(player => player.Level).Min();
            huntReq.TeamSize = huntReq.Players.Count();

            var checkHunt = _bll.CheckHuntReq(huntReq);

            if (checkHunt.Succeeded)
            {
                try
                {
                    var huntQueueLimit = _db.Hunts.Where(h => h.IdSituation == HuntSituationMap.Waiting
                    && h.IdAuthor == idAuthor).Count();

                    if (huntQueueLimit < 25)
                    {
                        var newHunt = _mapper.Map<Hunt>(huntReq);

                        newHunt.IdAuthor = idAuthor;
                        newHunt.IdSituation = HuntSituationMap.Waiting;

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
                catch(Exception ex)
                {
                   
                    Errors.Add("Error when saving hunt.");
                    response.Failed(Errors, StatusCodes.Status500InternalServerError);
                    response.SetException(ex);
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
                var updatedHunt = _mapper.Map<HuntRequest>(huntReq);
                updatedHunt.IdAuthor = idAuthor;

                try
                {
                    var huntInDb = await _db.Hunts
                        .SingleOrDefaultAsync(h =>
                        h.Id == updatedHunt.Id
                        && h.IdAuthor == updatedHunt.IdAuthor);

                    if (huntInDb != null)
                    {
                        updatedHunt.IdSituation = HuntSituationMap.Waiting;

                        _db.Hunts.Remove(huntInDb);
                        _db.Hunts.Add(_mapper.Map<Hunt>(CleanHuntKeysToUpdate(updatedHunt)));
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

            var huntToRemove = await _db.Hunts.SingleOrDefaultAsync(h => h.Id == idHunt
            && h.IdAuthor == idAuthor
            && h.IdSituation != HuntSituationMap.Deleted);

            if (huntToRemove != null)
            {
                try
                {
                    //Removido logicamente.
                    huntToRemove.IdSituation = HuntSituationMap.Deleted;
                    huntToRemove.IdAuthor = DefaultAuthorMap.id;
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
                var partialHunt = await _db.Hunts.FirstOrDefaultAsync(h => h.Id == idHunt
                && h.IdAuthor == idAuthor);


                if (partialHunt != null)
                {
                    var hunt = await _db.Hunts.Where(h => h.Id == idHunt && h.IdAuthor == idAuthor)
                        .Include(h => h.HuntClientVersions)
                        .Include(h => h.HuntMonsters)
                        .Include(h => h.HuntDescs)
                        .Include(h => h.Players).ThenInclude(h => h.Equipaments)
                        .Include(h => h.Players).ThenInclude(h => h.PlayerImbuements)
                        .Include(h => h.Players).ThenInclude(h => h.PlayerPreys)
                        .Include(h => h.Players).ThenInclude(h => h.PlayerItems)


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
        private HuntRequest CleanHuntKeysToUpdate(HuntRequest hunt)
        {
            hunt.Id = 0;

            foreach (var verions in hunt.HuntClientVersions)
            {
                verions.Id = 0;
            }

            foreach (var desc in hunt.HuntDescs)
            {
                desc.Id = 0;
            }

            foreach (var spcreq in hunt.HuntSpecialReqs)
            {
                spcreq.Id = 0;
            }

            foreach (var player in hunt.Players)
            {
                player.Id = 0;

                player.Equipaments.First().Id = 0;

                foreach (var imbue in player.PlayerImbuements)
                {
                    imbue.Id = 0;
                }

                foreach (var prey in player.PlayerPreys)
                {
                    prey.Id = 0;
                }

                foreach (var item in player.PlayerItems)
                {
                    item.Id = 0;
                }

            }

            foreach (var monster in hunt.HuntMonsters)
            {
                monster.Id = 0;
            }

            return hunt;

        }

    }
}
