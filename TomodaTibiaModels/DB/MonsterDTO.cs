using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;



namespace TomodaTibiaModels.DB
{
    public partial class MonsterDTO
    {  
        public string Img { get; set; }
        public string Name { get { return Formatting.ExtractName(Img); } }
    }
}
