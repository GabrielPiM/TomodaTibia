using System;
using System.Collections.Generic;

namespace TomodaTibiaModels.DB
{
    public partial class Autor
    {
        public Autor()
        {
            Hunt = new HashSet<Hunt>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string UrlSocial { get; set; }
        public string NomeMainChar { get; set; }

        public virtual ICollection<Hunt> Hunt { get; set; }
    }
}
