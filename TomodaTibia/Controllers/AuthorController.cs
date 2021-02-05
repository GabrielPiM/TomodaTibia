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
    public class AuthorController : Controller
    {

        private AuthorDataService DataService;
   
        public AuthorController(AuthorDataService dataService)
        {
            DataService = dataService;
       
        }

        [HttpPost("addAuthor")]
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
