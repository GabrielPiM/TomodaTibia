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
using TomodaTibiaAPI.DBContext;
using TomodaTibiaModels.Pesquisa;
using TomodaTibiaModels.Character;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace TomodaTibiaAPI.Controllers
{
    [Route("hunt/")]
    [ApiController]
    public class HuntController : ControllerBase
    {

        private readonly HuntDataService dataService;
        private readonly JsonReturn jsReturn;
        public HuntController(HuntDataService dataServ, JsonReturn jsonReturn)
        {
            dataService = dataServ;
            jsReturn = jsonReturn;
        }

        [HttpGet("{charName}")]
        public async Task<ActionResult> GetHunts(string charName)
        {
            //consulta os dados do personagem e hunt.
            var result = await dataService.Get(charName);

            //Retorna informações do personagem e as hunts (cards) recomendadas se encontrar.
            if (result != null)
                return jsReturn.OkJson(result);
            else
                return jsReturn.NotFoundJson("Personagem não encontrado.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHuntDetail(int id)
        {
            // //consulta os detalhes da hunt.
            var result = await dataService.GetDetail(id);

            //Retorna os detalhes se encontrar.
            if (result != null)
                return jsReturn.OkJson(result);
            else
                return jsReturn.NotFoundJson("Hunt não encontrada");
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(Hunt hunt)
        {
            var isAdd = await dataService.add(hunt);
            if (isAdd)
                return CreatedAtAction("autor/index", "Hunt Adicionada.");
            else
                return jsReturn.NotFoundJson("Aconteceu algum erro... tente novamente.");
        }
    }
}