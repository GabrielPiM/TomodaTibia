using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class AuthorDbReq
    {
        public AuthorDbReq()
        {
        
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }
        public bool? IsBan { get; set; }
        public bool? IsAdmin { get; set; }


    }
}
