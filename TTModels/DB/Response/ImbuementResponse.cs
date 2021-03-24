using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class ImbuementResponse
    {
        public ImbuementResponse()
        {
            Items = new List<ImbuementItemResponse>();

        }

        public string Category { get; set; }
        public string Img { get; set; }
        public string Desc { get; set; }
        public string Level { get; set; }
        public int Qty { get; set; }

        public List<ImbuementItemResponse> Items { get; set; }
    }
}
