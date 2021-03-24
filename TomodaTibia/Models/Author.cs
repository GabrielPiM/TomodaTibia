using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Author
    {
        public Author()
        {
            AuthorFavs = new HashSet<AuthorFav>();
            AuthorRats = new HashSet<AuthorRat>();
            Hunts = new HashSet<Hunt>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UrlSocial { get; set; }
        public string NameMainChar { get; set; }
        public bool? IsBan { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual ICollection<AuthorFav> AuthorFavs { get; set; }
        public virtual ICollection<AuthorRat> AuthorRats { get; set; }
        public virtual ICollection<Hunt> Hunts { get; set; }
    }
}
