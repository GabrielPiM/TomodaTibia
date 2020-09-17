using System;
using System.Collections.Generic;

namespace TomodaTibiaModels.DB
{
    public partial class HuntMonstro
    {
        public int? IdHunt { get; set; }
        public int? IdMonstro { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Monstro IdMonstroNavigation { get; set; }
    }
}
