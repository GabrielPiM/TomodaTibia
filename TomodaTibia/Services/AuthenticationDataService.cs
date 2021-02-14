using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TomodaTibiaAPI.Utils;
using TomodaTibiaModels.Account;
using TomodaTibiaModels.Account.Request;

namespace TomodaTibiaAPI.Services
{

    public interface IAuthenticationDataService
    {
        Task<Response<string>> SignIn(LoginRequest login, HttpContext http);
        Task<Response<string>> SingOut(HttpContext htpp);
    }

    public class AuthenticationDataService : IAuthenticationDataService
    {

        private readonly TomodaTibiaContext _db;

        public AuthenticationDataService(TomodaTibiaContext db)
        {
            _db = db;
        }

        public async Task<Response<string>> SignIn(LoginRequest login, HttpContext http)
        {
            var response = new Response<string>(string.Empty);

            try
            {
                var author = await _db.Authors.FirstOrDefaultAsync(a =>
                     a.Email == login.Email &&
                     a.Password == login.Password);

                if (author != null)
                {
                    await CookieSetup(author, http);
                    response.Message = "Sucessfuly Logged In.";
                }
                else
                {
                    response.Succeeded = false;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = "Wrong password or email.";
                }
            }
            catch
            {
                response.Succeeded = false;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Server failed to login.";
            }

            return response;
        }

        private async Task CookieSetup(Author auhtor, HttpContext http)
        {
            ClaimsIdentity Identity = new ClaimsIdentity("TomodaTibiaAPI");
            List<Claim> Claims = new List<Claim>();

            Claims.Add(new Claim(valueType: "Int", type: "Id", value: auhtor.Id.ToString()));

            Identity.AddClaims(Claims);

            ClaimsPrincipal Principal = new ClaimsPrincipal(new[] { Identity });

            await http.SignInAsync(Principal);
        }

        public async Task<Response<string>> SingOut(HttpContext htpp)
        {
            var response = new Response<string>(string.Empty);

            try
            {
                await htpp.SignOutAsync();
                response.Message = "Logged out.";
            }
            catch
            {
                response.Succeeded = false;
                response.Message = "Error logging out.";
                response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }
    }
}
