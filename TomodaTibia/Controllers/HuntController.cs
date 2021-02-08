using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomodaTibiaAPI;
using Microsoft.AspNetCore.Http;
using TomodaTibiaAPI.Services;
using System.Net.Http;


using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TomodaTibiaModels.DB.Request;
using TomodaTibiaAPI.Utils;

namespace TomodaTibiaAPI.Controllers
{


    public class HuntController : Controller
    {

        private readonly HuntDataService _dataService;

        public HuntController(HuntDataService dataService)
        {
            _dataService = dataService;

        }

        [HttpGet("huntJson")]
        public ActionResult GetHuntJson()
        {
            var hunt = new Hunt();

            hunt.HuntClientVersions.Add(new HuntClientVersion()
            {
                IdClientVersion = 1,
            });

            hunt.HuntImbuements.Add(new HuntImbuement()
            {
                IdImbuement = 0,
                IdImbuementLevel = 0,
                IdImbuementType = 0,
                Qty = 0
            });

            hunt.HuntItems.Add(new HuntItem()
            {
                IdItem = 0,
                Qty = 0
            });

            hunt.HuntPreys.Add(new HuntPrey()
            {
                IdPrey = 0,
                IdMonster = 0,
                ReccStar = 0
            });

            hunt.Players.Add(new Player()
            {
                Vocation = 1,
                Level = 0,
                Equipaments = new List<Equipament>() { new Equipament() { Id = 0 }, new Equipament() { Id = 0 } }

            });


            var huntReq = new HuntRequest();
            huntReq.HuntImbuements.Add(new HuntImbuementRequest());
            huntReq.HuntItems.Add(new HuntItemRequest());
            huntReq.Players.Add(new PlayerRequest());
            huntReq.Players.First().Equipaments.Add(new EquipamentRequest());
            huntReq.Players.First().Equipaments.Add(new EquipamentRequest());
            huntReq.HuntPreys.Add(new HuntPreyRequest());
            huntReq.HuntClientVersions.Add(new ClientVersionRequest());
            return Ok(huntReq);
        }

        //Consulta os dados do personagem e hunt.
        [HttpGet("search/{charName}")]
        public async Task<ActionResult> Hunts(string characterName)
        {
            var response = await _dataService.Search(characterName);

            //Retorna informações do personagem e as hunts (cards) recomendadas se encontrar.
            return StatusCode(response.StatusCode, response);
        }

        //consulta os detalhes da hunt.
        [HttpGet("{idHunt}")]
        public async Task<ActionResult> HuntDetail([FromRoute]int idHunt)
        {
            var response = await _dataService.Detail(idHunt);

            //Retorna os detalhes se encontrar.
            return StatusCode(response.StatusCode, response);
        }

        //Adiocina uma hunt.
        [Authorize]
        [HttpPost("hunt")]
        public async Task<ActionResult> AddHunt([FromBody] HuntRequest hunt)
        {

            hunt.IdAuthor = int.Parse(User.Claims
                .FirstOrDefault(x => x.Type == "Id").Value
                .ToString());

            var response = await _dataService.Add(hunt);

            return StatusCode(response.StatusCode, response);
        }

        //Remove uma hunt.
        [Authorize]
        [HttpDelete("{idHunt}")]
        public async Task<ActionResult> RemoveHunt(int idHunt)
        {
            var idAuthor = int.Parse(User.Claims
                .FirstOrDefault(x => x.Type == "Id").Value
                .ToString());

            var response = await _dataService.Remove(idHunt, idAuthor);
     
            return StatusCode(response.StatusCode, response);
        }

        //Remove uma hunt.
        [Authorize]
        [HttpPut("hunt")]
        public async Task<ActionResult> UpdateHunt([FromBody] HuntRequest hunt)
        {
            var idAuthor = int.Parse(User.Claims
                .FirstOrDefault(x => x.Type == "Id").Value
                .ToString());

            var response = await _dataService.Update(hunt,idAuthor);

            return StatusCode(response.StatusCode, response);
        }

    }
}