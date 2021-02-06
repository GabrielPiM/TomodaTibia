using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class ImbuementItemResponse
    {
        public int Id { get; set; }
        public int? IdImbuement { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdItem { get; set; }
        public int Qty { get; set; }

    }
}
