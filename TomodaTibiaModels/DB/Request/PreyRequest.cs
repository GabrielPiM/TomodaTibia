using System;
using System.Collections.Generic;
using System.Globalization;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaModels.DB.Request
{
    public partial class PreyRequest
    {
        public string Img { get; set; }
        public int ReccStars { get; set; }
        public string Type { get { return Formatting.ExtractName(Img); } }
        public MonsterRequest Monster { get; set; }

    }
}
