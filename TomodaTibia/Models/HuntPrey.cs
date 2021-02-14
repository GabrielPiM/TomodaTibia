using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.Models
{
    public partial class HuntPrey
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdPrey { get; set; }
        public int? IdMonster { get; set; }
        public int ReccStar { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Monster IdMonsterNavigation { get; set; }
        public virtual Prey IdPreyNavigation { get; set; }
    }
}
