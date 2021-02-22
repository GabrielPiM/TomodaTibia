using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class HuntLoot
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdItem { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Item IdItemNavigation { get; set; }
    }
}
