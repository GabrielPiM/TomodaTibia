using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntImbuementRequest
    {
        public int Id { get; set; }
        public int IdImbuement { get; set; }
        public int IdImbuementLevel { get; set; }
        public int IdImbuementType { get; set; }
        public int Qty { get; set; }

    }
}
