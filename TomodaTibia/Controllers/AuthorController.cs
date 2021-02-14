using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;
using TomodaTibiaAPI.Utils;
using TomodaTibiaModels.DB.Request;

namespace TomodaTibiaAPI.Controllers
{

    [Route("authors")]
    public class AuthorController : Controller
    {

        private AuthorDataService _dataService;
        private readonly CurrentUser _user;

        public AuthorController(AuthorDataService dataService, CurrentUser user)
        {
            _dataService = dataService;
            _user = user;
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthor([FromBody] AuthorRequest author)
        {
            var response = await _dataService.Add(author);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> RemoveAuthor([FromBody] AuthorRequest author)
        {
            author.Id = _user.IdAuthor(HttpContext);

            var response = await _dataService.Remove(author);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateAuthor([FromBody] AuthorRequest author)
        {
            author.Id = _user.IdAuthor(HttpContext);

            var response = await _dataService.Update(author);

            return StatusCode(response.StatusCode, response);
        }


    }
}
