using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class AuthorResponse
    {
        public AuthorResponse()
        {
            Hunts = new HashSet<HuntResponse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }

        public virtual ICollection<HuntResponse> Hunts { get; set; }
    }
}
