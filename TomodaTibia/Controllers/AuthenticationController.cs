using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;
using TomodaTibiaModels.Account;


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
        public IActionResult secretAPI() => Ok("secret api");


        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate(LoginDTO login)
        {
            var accExists = await _dataService.Authentication(login);

            if (accExists != null)
            {
                await HttpContext.SignInAsync(accExists);
                return Ok("Sucessfuly Logged In.");
            }

            return Unauthorized("Failed to login.");
        }


    }
}
