using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;
using TomodaTibiaModels.DB.Request;

namespace TomodaTibiaAPI.Controllers
{
    public class AuthorController : Controller
    {

        private AuthorDataService _dataService;
   
        public AuthorController(AuthorDataService dataService)
        {
            _dataService = dataService;
       
        }

        [HttpPost("Author")]
        public async Task<ActionResult> AddAuthor([FromBody]AuthorRequest author)
        {
            var response = await _dataService.Add(author);

            return StatusCode(response.StatusCode, response);   
        }
    }
}
