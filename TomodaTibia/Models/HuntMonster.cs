using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.Models
{
    public partial class HuntMonster
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdMonster { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Monster IdMonsterNavigation { get; set; }
    }
}
