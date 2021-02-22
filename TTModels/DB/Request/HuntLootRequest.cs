using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntLootRequest
    {
        public int Id { get; set; }
        public int? IdItem { get; set; }
    }
}
