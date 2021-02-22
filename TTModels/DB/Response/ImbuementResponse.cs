using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class ImbuementResponse
    {

        public ImbuementResponse()
        {
            Items = new List<ItemResponse>();
        }

        public string Category { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public string Level { get; set; }
        public int Qty { get; set; }
        public string Img { get; set; }


        public List<ItemResponse> Items { get; set; }
    }
}
