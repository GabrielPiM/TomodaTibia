using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TomodaTibiaAPI.Utils
{
    public class CurrentUser
    {


        [Authorize]
        public int IdAuthor(HttpContext http)
        {     
            return int.Parse(http.User.Claims
                .FirstOrDefault(x => x.Type == "Id").Value
                .ToString());
        }
    }
}
