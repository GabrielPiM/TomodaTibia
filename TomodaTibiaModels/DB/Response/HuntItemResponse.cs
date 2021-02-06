using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Response
{
    public partial class HuntItemResponse
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdItem { get; set; }
        public int? Qty { get; set; }

    }
}
