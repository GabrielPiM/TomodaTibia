using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class AuthorFav
    {
        public int Id { get; set; }
        public int IdAuthor { get; set; }
        public int IdHunt { get; set; }

    }
}
