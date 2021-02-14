using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.Models
{
    public partial class Monster
    {
        public Monster()
        {
            HuntMonsters = new HashSet<HuntMonster>();
            HuntPreys = new HashSet<HuntPrey>();
        }

        public int Id { get; set; }
        public string Img { get; set; }
        public bool? IsPray { get; set; }

        public virtual ICollection<HuntMonster> HuntMonsters { get; set; }
        public virtual ICollection<HuntPrey> HuntPreys { get; set; }
    }
}
