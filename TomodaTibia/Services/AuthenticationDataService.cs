using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TomodaTibiaModels.Account;
using TomodaTibiaModels.Account.Request;

namespace TomodaTibiaAPI.Services
{

    public interface IAuthenticationDataService
    {
        Task<ClaimsPrincipal> Authenticate(LoginRequest login);
    }

    public class AuthenticationDataService : IAuthenticationDataService
    {

        private readonly TomodaTibiaContext _db;

        public AuthenticationDataService(TomodaTibiaContext db)
        {
            _db = db;
        }

        public async Task<ClaimsPrincipal> Authenticate(LoginRequest login)
        {
            var author = await _db.Authors.FirstOrDefaultAsync(a =>
               a.Email == login.Email &&
               a.Password == login.Password
            );

            return author != null ? (SignIn(author)) : null;
        }

        public ClaimsPrincipal SignIn(Author auhtor)
        {
            ClaimsIdentity Identity = new ClaimsIdentity("TomodaTibiaAPI");
            List<Claim> Claims = new List<Claim>();

            Claims.Add(new Claim(valueType: "Int", type: "Id", value: auhtor.Id.ToString()));    

            Identity.AddClaims(Claims);

            ClaimsPrincipal Principal = new ClaimsPrincipal(new[] { Identity });

            return Principal;
        }
    }
}
