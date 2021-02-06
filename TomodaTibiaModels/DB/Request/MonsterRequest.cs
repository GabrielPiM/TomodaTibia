using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;



namespace TomodaTibiaModels.DB.Request
{
    public partial class MonsterRequest
    {  
        public string Img { get; set; }
        public string Name { get { return Formatting.ExtractName(Img); } }
    }
}
