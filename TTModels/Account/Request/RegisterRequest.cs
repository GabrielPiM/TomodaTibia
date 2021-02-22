using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.Account.Request
{
    public partial class RegisterRequest
    {

        public RegisterRequest()
        {
          
        }
       
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }
        
    }
}
