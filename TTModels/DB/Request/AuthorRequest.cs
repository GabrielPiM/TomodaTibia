using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class AuthorRequest
    {
        public AuthorRequest()
        {
         
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }           
    }
}
