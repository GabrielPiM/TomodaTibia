using System;
using System.Collections.Generic;
using System.Globalization;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaModels.DB.Response
{
    public partial class PreyResponse
    {
        public string Img { get; set; }
        public int ReccStars { get; set; }
        public string Type { get { return Formatting.ExtractName(Img); } }
        public MonsterResponse Monster { get; set; }

    }
}
