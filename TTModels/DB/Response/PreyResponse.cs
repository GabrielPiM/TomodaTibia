using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class PreyResponse
    {
        public PreyResponse()
        {
          
        }

        
        public string TypeImg { get; set; }
        public string MonsterImg { get; set; }


        public string Title
        {
            get
            {
                return Formatting.ExtractName(MonsterImg);
            }
        }

        public string Type
        {
            get
            {
                return Formatting.ExtractName(TypeImg);
            }
        }

        public int Stars { get; set; }
    }
}
