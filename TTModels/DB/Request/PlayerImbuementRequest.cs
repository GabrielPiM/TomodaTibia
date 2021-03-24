using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class PlayerImbuementRequest
    {
        public int Id { get; set; }
        public int IdImbuement { get; set; }
        public int IdImbuementLevel { get; set; }
        public int IdImbuementType { get; set; }
        public int Qty { get; set; }

    }
}
