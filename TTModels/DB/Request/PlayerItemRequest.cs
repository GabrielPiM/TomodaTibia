using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class PlayerItemRequest
    {
        public int Id { get; set; }
        public int IdItem { get; set; }
        public int? Qty { get; set; }

    }
}
