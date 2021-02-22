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
using TomodaTibiaModels.Hunt.Response;
using TomodaTibiaModels.DB.Response;
using TomodaTibiaAPI.BLL;
using TomodaTibiaAPI.Utils.Pagination;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaAPI.Controllers
{

    [Route("hunts")]
    public class HuntController : Controller
    {

        private readonly HuntDataService _dataService;
        private readonly CurrentUserService _user;


        public HuntController(HuntDataService dataService, CurrentUserService user)
        {
            _dataService = dataService;
            _user = user;
        }


        //Consulta os dados do personagem e hunt.
        [HttpPost("search")]
        public async Task<ActionResult> Search([FromBody] SearchParameterRequest parameters)
        {
            var response = new Response<SearchResponse>();
           
            //Nova consulta de hunts.
            response = await _dataService.Search(parameters);

            //Retorna informações do personagem e as hunts (cards) recomendadas se encontrar.
            return StatusCode(response.StatusCode, response);
        }

        //consulta os detalhes da hunt.
        [HttpGet("{idHunt}")]
        public async Task<ActionResult> HuntDetail(int idHunt)
        {
            var response = await _dataService.HuntDetail(idHunt);

            //Retorna os detalhes da hunt se encontrar.
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("req/{idHunt}")]
        public async Task<ActionResult> GetHuntToUpdate(int idHunt)
        {
            var response = await _dataService.HuntToUpdate(idHunt, _user.IdAuthor(HttpContext));

            //Retorna os detalhes da hunt se encontrar.
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("author/{idAuthor}")]
        public async Task<ActionResult> AuthorHuntsList(int idAuthor)
        {
            var response = await _dataService.AuthorHuntList(_user.IdAuthor(HttpContext));

            //Retorna a lista de hunts do determinado autor.
            return StatusCode(response.StatusCode, response);
        }

        //Adiocina uma hunt.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddHunt([FromBody] HuntRequest hunt)
        {
            var response = await _dataService.Add(hunt, _user.IdAuthor(HttpContext));

            return StatusCode(response.StatusCode, response);
        }

        //Remove uma hunt.
        [Authorize]
        [HttpDelete("{idHunt}")]
        public async Task<ActionResult> RemoveHunt(int idHunt)
        {
            var response = await _dataService.Remove(idHunt, _user.IdAuthor(HttpContext));

            return StatusCode(response.StatusCode, response);
        }

        //Atualiza uma hunt.
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateHunt([FromBody] HuntRequest hunt)
        {
            var response = await _dataService.Update(hunt, _user.IdAuthor(HttpContext));

            return StatusCode(response.StatusCode, response);
        }

    }
}