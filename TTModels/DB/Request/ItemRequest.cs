using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaModels.DB.Request
{
    public partial class ItemRequest
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public int? Qty { get; set; }
        public string Name
        {
            get
            {
                return Formatting.ExtractName(Img);
            }
        }
    }
}
