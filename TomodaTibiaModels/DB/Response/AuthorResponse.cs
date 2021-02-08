using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class AuthorResponse
    {
        public AuthorResponse()
        {
       
        }
     
        public string Name { get; set; }
        public string Email { get; set; }    
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }
       
    }
}
