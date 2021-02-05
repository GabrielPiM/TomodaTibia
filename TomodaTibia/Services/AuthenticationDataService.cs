using EFDataAcessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TomodaTibiaModels.Account;


namespace TomodaTibiaAPI.Services
{

    public interface IAuthenticationDataService
    {
        Task<ClaimsPrincipal> Authentication(LoginDTO login);
    }

    public class AuthenticationDataService : IAuthenticationDataService
    {

        private readonly TomodaTibiaContext _db;

        public AuthenticationDataService(TomodaTibiaContext db)
        {
            _db = db;
        }

        public async Task<ClaimsPrincipal> Authentication(LoginDTO login)
        {
            var author = await _db.Authors.FirstOrDefaultAsync(a =>
               a.Email == login.Email &&
               a.Password == login.Password
            );

           return author != null ? (SignIn(author)) : null;                
        }

        public ClaimsPrincipal SignIn(Author auhtor)
        {
            ClaimsIdentity identity = new ClaimsIdentity("TomodaTibiaAPI");

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier , auhtor.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, auhtor.Name));
            identity.AddClaim(new Claim(ClaimTypes.Email, auhtor.Email));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, auhtor.NameMainChar));

            ClaimsPrincipal principal = new ClaimsPrincipal(new[] { identity});

            return principal;   
        }
    }
}
