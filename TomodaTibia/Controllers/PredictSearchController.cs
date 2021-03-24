using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;
using TomodaTibiaModels.Hunt.Response.AddHunt.PredictSearch;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaAPI.Controllers
{
    [Route("/PredictSearch")]
  
    public class PredictSearchController : Controller
    {

        private readonly PredictSearchDataService _dataService;

        public PredictSearchController(PredictSearchDataService dataService)
        {
            _dataService = dataService;
        }


        [Authorize]
        [HttpGet("location/{name}")]
        public async Task<ActionResult> Location(string name)
        {
            var response = await _dataService.SearchLocation(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("monster/{name}")]
        public async Task<ActionResult> Monster(string name)
        {
            var response = await _dataService.SearchMonster(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("book/{name}")]
        public async Task<ActionResult> Book(string name)
        {
            var response = await _dataService.SearchBook(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("huntingplace/{name}")]
        public async Task<ActionResult> HuntingPlace(string name)
        {
            var response = await _dataService.SearchHuntingPlace(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("quest/{name}")]
        public async Task<ActionResult> Quest(string name)
        {
            var response = await _dataService.SearchQuest(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("achivement/{name}")]
        public async Task<ActionResult> Achivement(string name)
        {
            var response = await _dataService.SearchAchivement(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("key/{name}")]
        public async Task<ActionResult> Key(string name)
        {
            var response = await _dataService.SearchKey(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("mount/{name}")]
        public async Task<ActionResult> Mount(string name)
        {
            var response = await _dataService.SearchMount(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("object/{name}")]
        public async Task<ActionResult> Object(string name)
        {
            var response = await _dataService.SearchObject(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("spell/{name}")]
        public async Task<ActionResult> Spell(string name)
        {
            var response = await _dataService.SearchSpell(name);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("item/{name}")]
        public async Task<ActionResult> Item(string name)
        {
            var response = await _dataService.SearchItem(name);

            return StatusCode(response.StatusCode, response);
        }
    }
}
