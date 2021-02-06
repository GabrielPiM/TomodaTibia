using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;



namespace TomodaTibiaModels.DB.Response
{
    public partial class MonsterResponse
    {  
        public string Img { get; set; }
        public string Name { get { return Formatting.ExtractName(Img); } }
    }
}
