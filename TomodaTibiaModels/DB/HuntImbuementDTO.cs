using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class HuntImbuementDTO
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdImbuement { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdImbuementType { get; set; }
        public int Qty { get; set; }

        public virtual HuntDTO IdHuntNavigation { get; set; }
        public virtual ImbuementLevelDTO IdImbuementLevelNavigation { get; set; }
        public virtual ImbuementDTO IdImbuementNavigation { get; set; }
        public virtual ImbuementTypeDTO IdImbuementTypeNavigation { get; set; }
    }
}
