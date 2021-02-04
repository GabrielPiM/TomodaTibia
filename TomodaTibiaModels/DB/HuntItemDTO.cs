using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class HuntItemDTO
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdItem { get; set; }
        public int? Qty { get; set; }

        public virtual HuntDTO IdHuntNavigation { get; set; }
        public virtual ItemDTO IdItemNavigation { get; set; }
    }
}
