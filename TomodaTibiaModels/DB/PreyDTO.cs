using System;
using System.Collections.Generic;
using System.Globalization;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaModels.DB
{
    public partial class PreyDTO
    {
        public string Img { get; set; }
        public int ReccStars { get; set; }
        public string Type { get { return Formatting.ExtractName(Img); } }
        public MonsterDTO Monster { get; set; }

    }
}
