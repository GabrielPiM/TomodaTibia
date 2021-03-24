using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Situation
    {
        public Situation()
        {
            Hunts = new HashSet<Hunt>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Hunt> Hunts { get; set; }
    }
}
