using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class Player
    {
        public Player()
        {
            Equipaments = new HashSet<Equipament>();
        }

        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int Vocation { get; set; }
        public int Level { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual ICollection<Equipament> Equipaments { get; set; }
    }
}
