using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TomodaTibiaModels.DB
{
    public partial class OutroItem
    {
        public int? IdHunt { get; set; }
        public int? IdItem { get; set; }
        public int? Qtd { get; set; }
        public int Id { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}
