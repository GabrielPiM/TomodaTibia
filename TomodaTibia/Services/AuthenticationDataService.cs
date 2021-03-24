
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
using TomodaTibiaModels.Utils;
using TomodaTibiaAPI.EntityFramework;

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
        private List<string> Errors;

        public AuthenticationDataService(TomodaTibiaContext db)
        {
            _db = db;
            Errors = new List<string>();
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

                    response.Sucess("Sucessfuly Logged In.", string.Empty);
                }
                else
                {
                    Errors.Add("Wrong password or email.");
                    response.Failed(Errors, StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                Errors.Add("Server failed to login.");
                response.Failed(Errors, StatusCodes.Status500InternalServerError);
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
                response.Sucess("Logged out.", string.Empty);
            }
            catch
            {
                Errors.Add("Error logging out.");
                response.Failed(Errors, StatusCodes.Status500InternalServerError);
            }

            return response;
        }
    }
}
