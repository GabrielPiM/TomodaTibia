using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB
{
    public partial class ImbuementDTO
    {

        public ImbuementDTO()
        {
            Items = new List<ItemDTO>();
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
        public string Level { get; set; }
        public int Qty { get; set; }
        public string Img { get; set; }


        public List<ItemDTO> Items { get; set; }
    }
}
