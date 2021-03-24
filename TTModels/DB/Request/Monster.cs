using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class Monster
    {
        public Monster()
        {
            HuntMonsters = new HashSet<HuntMonsterRequest>();
            MonsterLoots = new HashSet<MonsterLoot>();
            PlayerPreys = new HashSet<PlayerPreyRequest>();
        }

        public int Id { get; set; }
        public string Img { get; set; }
        public bool? IsPray { get; set; }

        public virtual ICollection<HuntMonsterRequest> HuntMonsters { get; set; }
        public virtual ICollection<MonsterLoot> MonsterLoots { get; set; }
        public virtual ICollection<PlayerPreyRequest> PlayerPreys { get; set; }
    }
}
