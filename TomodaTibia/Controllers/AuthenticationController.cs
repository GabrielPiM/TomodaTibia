using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomodaTibiaAPI.Services;
using TomodaTibiaAPI.Utils;
using TomodaTibiaModels.Account.Request;

namespace TomodaTibiaAPI.Controllers
{

    [Route("auths")]
    public class AuthenticationController : Controller
    {

        private readonly AuthenticationDataService _dataService;

        public AuthenticationController(AuthenticationDataService dataService, CurrentUserService user)
        {
            _dataService = dataService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginReq)
        {
            var response = await _dataService.SignIn(loginReq, HttpContext);

            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _dataService.SingOut(HttpContext);

            return StatusCode(response.StatusCode, response);
        }
    }
}
