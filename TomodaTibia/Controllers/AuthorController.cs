using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;

namespace TomodaTibiaAPI.Controllers
{
    [Route("author/")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        private AuthorDataService DataService;
        private JsonReturn JsReturn;

        public AuthorController(AuthorDataService dataService, JsonReturn jsReturn)
        {
            DataService = dataService;
            JsReturn = jsReturn;
        }

        [HttpPost("add")]
        public async Task<ActionResult> Add(Author author)
        {
            var result = await DataService.Add(author);
            Author newAuthor = (Author)result;

            if (newAuthor != null)
                return Ok(newAuthor);
            else
                return NotFound();
        }

    }
}
