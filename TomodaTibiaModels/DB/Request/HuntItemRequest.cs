using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntItemRequest
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdItem { get; set; }
        public int? Qty { get; set; }


    }
}
