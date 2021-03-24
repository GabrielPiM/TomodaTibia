using System;
using System.Collections.Generic;
using TomodaTibiaModels.Utils;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public class HuntMonsterResponse
    {
        public HuntMonsterResponse()
        {
            Loot = new List<MonsterLootResponse>();
        }

        public string Name
        {
            get
            {
                return Formatting.ExtractName(Img);
            }
        }

        public string Img { get; set; }
        public int? Qty { get; set; }

        public List<MonsterLootResponse> Loot { get; set; }


    }
}
