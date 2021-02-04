using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB
{
    public partial class ImbuementItemDTO
    {
        public int Id { get; set; }
        public int? IdImbuement { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdItem { get; set; }
        public int Qty { get; set; }

        public virtual ImbuementLevelDTO IdImbuementLevelNavigation { get; set; }
        public virtual ImbuementDTO IdImbuementNavigation { get; set; }
        public virtual ItemDTO IdItemNavigation { get; set; }
    }
}
