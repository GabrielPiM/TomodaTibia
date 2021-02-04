using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class HuntImbuement
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdImbuement { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdImbuementType { get; set; }
        public int Qty { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual ImbuementLevel IdImbuementLevelNavigation { get; set; }
        public virtual Imbuement IdImbuementNavigation { get; set; }
        public virtual ImbuementType IdImbuementTypeNavigation { get; set; }
    }
}
