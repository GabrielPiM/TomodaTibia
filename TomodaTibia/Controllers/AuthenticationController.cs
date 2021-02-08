using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;
using TomodaTibiaModels.Account.Request;

namespace TomodaTibiaAPI.Controllers
{
    public class AuthenticationController : Controller
    {

        private AuthenticationDataService _dataService;

        public AuthenticationController(AuthenticationDataService dataService)
        {
            _dataService = dataService;
        }


        [Authorize]
        [HttpGet("secret")]
        public IActionResult secretAPI() => Ok("secret api \n" + User.Claims
                .FirstOrDefault(x => x.Type == "Id").Value
                .ToString());


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest login)
        {
            var accExists = await _dataService.Authenticate(login);

            if (accExists != null)
            {
                await HttpContext.SignInAsync(accExists);
                return Ok("Sucessfuly Logged In.");
            }

            return Unauthorized("Failed to login.");
        }


    }
}
