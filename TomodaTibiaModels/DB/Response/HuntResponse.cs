using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class HuntResponse
    {
        public HuntResponse()
        {
            Versions = new List<String>();
            Imbuements = new List<ImbuementResponse>();
            OtherItems = new List<ItemResponse>();
            Preys = new List<PreyResponse>();
            Players = new List<PlayerResponse>();
            HuntMonsters = new List<MonsterResponse>();
        }
        
        public string Name { get; set; }
        public int NivelMinReq { get; set; }
        public int XpHr { get; set; }
        public int QtyPlayer { get; set; }
        public string VideoTutorialUrl { get; set; }
        public string DescHunt { get; set; }
        public int? Difficulty { get; set; }
        public int? Rating { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsPremium { get; set; }


        public List<PlayerResponse> Players { get; set; }
        public List<ItemResponse> OtherItems { get; set; }
        public List<PreyResponse> Preys { get; set; }
        public List<ImbuementResponse> Imbuements { get; set; }        
        public List<String> Versions { get; set; }
        public List<MonsterResponse> HuntMonsters { get; set; }
    }
}
