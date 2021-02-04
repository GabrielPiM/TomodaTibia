using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB
{
    public partial class AuthorDTO
    {
        public AuthorDTO()
        {
            Hunts = new HashSet<HuntDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }

        public virtual ICollection<HuntDTO> Hunts { get; set; }
    }
}
