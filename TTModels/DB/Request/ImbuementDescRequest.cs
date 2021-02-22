using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Request
{
    public partial class ImbuementDescRequest
    {
        public int Id { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdImbuementType { get; set; }
        public string Value { get; set; }

    }
}
