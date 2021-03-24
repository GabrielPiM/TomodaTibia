using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class ImbuementItem
    {
        public int Id { get; set; }
        public int IdImbuement { get; set; }
        public int IdImbuementLevel { get; set; }
        public int IdItem { get; set; }
        public int Qty { get; set; }

        public virtual ImbuementLevel IdImbuementLevelNavigation { get; set; }
        public virtual Imbuement IdImbuementNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}
