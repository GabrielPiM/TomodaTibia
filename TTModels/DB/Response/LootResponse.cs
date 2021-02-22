using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class LootResponse
    {
        public string Img { get; set; }
        public string Name { get { return Formatting.ExtractName(Img); } }
    }
}
