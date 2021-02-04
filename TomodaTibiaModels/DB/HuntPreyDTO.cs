using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class HuntPreyDTO
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdPrey { get; set; }
        public int? IdMonster { get; set; }
        public int ReccStar { get; set; }

        public virtual HuntDTO IdHuntNavigation { get; set; }
        public virtual MonsterDTO IdMonsterNavigation { get; set; }
        public virtual PreyDTO IdPreyNavigation { get; set; }
    }
}
