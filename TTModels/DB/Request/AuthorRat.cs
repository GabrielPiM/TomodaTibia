using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class AuthorRat
    {
        public int Id { get; set; }
        public int IdAuthor { get; set; }
        public int IdHunt { get; set; }
        public int Rating { get; set; }

        public virtual AuthorDbReq IdAuthorNavigation { get; set; }
        public virtual HuntRequest IdHuntNavigation { get; set; }
    }
}
