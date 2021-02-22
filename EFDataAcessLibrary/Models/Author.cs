using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class Author
    {
        public Author()
        {
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

        public virtual ICollection<Hunt> Hunts { get; set; }
    }
}
