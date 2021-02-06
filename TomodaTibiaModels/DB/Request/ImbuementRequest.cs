using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class ImbuementRequest
    {

        public ImbuementRequest()
        {
            Items = new List<ItemRequest>();
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public string Level { get; set; }
        public int Qty { get; set; }
        public string Img { get; set; }


        public List<ItemRequest> Items { get; set; }
    }
}
