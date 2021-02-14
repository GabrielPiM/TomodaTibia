using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntRequest
    {


        public HuntRequest()
        {
            HuntImbuements = new List<HuntImbuementRequest>();
            HuntItems = new List<HuntItemRequest>();
            HuntPreys = new List<HuntPreyRequest>();
            Players = new List<PlayerRequest>();
            HuntClientVersions = new List<HuntClientVersionRequest>();
            HuntMonsters = new List<HuntMonsterRequest>();
        }

        public int Id { get; set; }       
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


        public List<HuntClientVersionRequest> HuntClientVersions { get; set; }      
        public List<HuntImbuementRequest> HuntImbuements { get; set; }
        public List<HuntItemRequest> HuntItems { get; set; }
        public List<HuntPreyRequest> HuntPreys { get; set; }
        public List<HuntMonsterRequest> HuntMonsters { get; set; }
        public List<PlayerRequest> Players { get; set; } 
      
        


    }
}
