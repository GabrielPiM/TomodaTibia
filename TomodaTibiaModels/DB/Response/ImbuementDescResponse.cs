using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Response
{
    public partial class ImbuementDescResponse
    {
        public int Id { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdImbuementType { get; set; }
        public string Value { get; set; }

    }
}
