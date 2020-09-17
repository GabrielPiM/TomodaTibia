using System;
using System.Collections.Generic;

namespace TomodaTibiaModels.DB
{
    public partial class Player
    {
        public Player()
        {
            Equipamento = new HashSet<Equipamento>();
        }

        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int Vocacao { get; set; }
        public int Nivel { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual ICollection<Equipamento> Equipamento { get; set; }
    }
}
