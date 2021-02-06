using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Response
{
    public partial class HuntImbuementResponse
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdImbuement { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdImbuementType { get; set; }
        public int Qty { get; set; }


    }
}
