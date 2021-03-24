using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Prey
    {
        public Prey()
        {
            PlayerPreys = new HashSet<PlayerPrey>();
        }

        public int Id { get; set; }
        public string Img { get; set; }

        public virtual ICollection<PlayerPrey> PlayerPreys { get; set; }
    }
}
