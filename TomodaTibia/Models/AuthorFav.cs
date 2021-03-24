using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class AuthorFav
    {
        public int Id { get; set; }
        public int IdAuthor { get; set; }
        public int IdHunt { get; set; }

        public virtual Author IdAuthorNavigation { get; set; }
        public virtual Hunt IdHuntNavigation { get; set; }
    }
}
