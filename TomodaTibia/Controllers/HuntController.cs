using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomodaTibiaAPI;
using Microsoft.AspNetCore.Http;
using TomodaTibiaAPI.Services;
using System.Net.Http;
using TomodaTibiaModels.Hunt;
using TomodaTibiaModels.DB;

using TomodaTibiaModels.Pesquisa;
using TomodaTibiaModels.Character;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TomodaTibiaAPI.Controllers
{


    public class HuntController : Controller
    {

        private readonly HuntDataService dataService;

        public HuntController(HuntDataService dataServ)
        {
            dataService = dataServ;

        }

        [HttpGet("{charName}")]
        public async Task<ActionResult> GetHunts(string charName)
        {
            //consulta os dados do personagem e hunt.
            var result = await dataService.Get(charName);

            //Retorna informações do personagem e as hunts (cards) recomendadas se encontrar.
            if (result != null)
                return Ok(result);
            else
                return NotFound("Personagem não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHuntDetail(int id)
        {
            // //consulta os detalhes da hunt.
            var result = await dataService.GetDetail(id);

            //Retorna os detalhes se encontrar.
            if (result != null)
                return Ok(result);
            else
                return NotFound("Hunt não encontrada.");
        }

        [Authorize]
        [HttpPost("addHunt")]
        public async Task<ActionResult> Add(Hunt hunt)
        {
            hunt.IdAuthor = int.Parse(User.Claims
                .Where(x => x.Type == ClaimTypes.NameIdentifier)
                .FirstOrDefault().ToString());

            var result = await dataService.Add(hunt);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}