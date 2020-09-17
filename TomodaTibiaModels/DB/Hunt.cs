using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomodaTibiaModels.DB
{
    public partial class Hunt
    {
        public Hunt()
        {
            OutroItem = new HashSet<OutroItem>();
            Player = new HashSet<Player>();
        }

        public int Id { get; set; }
        public int? IdAutor { get; set; }
        public string Nome { get; set; }
        public int NivelMinReq { get; set; }
        public int? QtdPlayer { get; set; }
        public bool? Premium { get; set; }
        public string VideoTutorial { get; set; }
        public string DescHunt { get; set; }
        public bool? IsValid { get; set; }

        public virtual Autor IdAutorNavigation { get; set; }
        public virtual ICollection<OutroItem> OutroItem { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}
