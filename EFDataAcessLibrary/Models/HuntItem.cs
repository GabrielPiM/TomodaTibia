using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class HuntItem
    {
        public int Id { get; set; }
        public int IdHunt { get; set; }
        public int IdItem { get; set; }
        public int? Qty { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}
