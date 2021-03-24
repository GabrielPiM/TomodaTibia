using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class PlayerItem
    {
        public int Id { get; set; }
        public int IdPlayer { get; set; }
        public int IdItem { get; set; }
        public int? Qty { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual Player IdPlayerNavigation { get; set; }
    }
}
