using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class Situation
    {
        public Situation()
        {
            Hunts = new HashSet<HuntRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntRequest> Hunts { get; set; }
    }
}
