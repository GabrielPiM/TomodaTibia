using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class PlayerPrey
    {
        public int Id { get; set; }
        public int? IdPlayer { get; set; }
        public int IdPrey { get; set; }
        public int IdMonster { get; set; }
        public int ReccStar { get; set; }

        public virtual Monster IdMonsterNavigation { get; set; }
        public virtual Player IdPlayerNavigation { get; set; }
        public virtual Prey IdPreyNavigation { get; set; }
    }
}
