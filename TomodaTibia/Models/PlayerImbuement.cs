using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class PlayerImbuement
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int IdImbuement { get; set; }
        public int IdImbuementLevel { get; set; }
        public int IdImbuementType { get; set; }
        public int Qty { get; set; }

        public virtual ImbuementLevel IdImbuementLevelNavigation { get; set; }
        public virtual Imbuement IdImbuementNavigation { get; set; }
        public virtual Player IdPlayerNavigation { get; set; }
    }
}
