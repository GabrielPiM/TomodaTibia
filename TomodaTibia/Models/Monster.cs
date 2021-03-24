using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Monster
    {
        public Monster()
        {
            HuntMonsters = new HashSet<HuntMonster>();
            MonsterLoots = new HashSet<MonsterLoot>();
            PlayerPreys = new HashSet<PlayerPrey>();
        }

        public int Id { get; set; }
        public string Img { get; set; }
        public bool? IsPray { get; set; }

        public virtual ICollection<HuntMonster> HuntMonsters { get; set; }
        public virtual ICollection<MonsterLoot> MonsterLoots { get; set; }
        public virtual ICollection<PlayerPrey> PlayerPreys { get; set; }
    }
}
