using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class HuntMonsterDTO
    {
        public int? IdHunt { get; set; }
        public int? IdMonster { get; set; }

        public virtual HuntDTO IdHuntNavigation { get; set; }
        public virtual MonsterDTO IdMonsterNavigation { get; set; }
    }
}
