using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntRequest
    {
        public HuntRequest()
        {
          
            HuntClientVersions = new List<HuntClientVersionRequest>();
            HuntDescs = new List<HuntDescRequest>();
            HuntMonsters = new List<HuntMonsterRequest>();
            HuntSpecialReqs = new List<HuntSpecialReqRequest>();
            Players = new List<PlayerRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdAuthor { get; set; }
        public string Location { get; set; }
        public int LevelMinReq { get; set; }
        public int XpHr { get; set; }
        public string TutorialVideoUrl { get; set; }
        public int? Difficulty { get; set; }
        public int TeamSize { get; set; }
        public int IdSituation { get; set; }
        public bool? IsPremium { get; set; }
  

        public virtual List<HuntClientVersionRequest> HuntClientVersions { get; set; }
        public virtual List<HuntDescRequest> HuntDescs { get; set; }
        public virtual List<HuntMonsterRequest> HuntMonsters { get; set; }
        public virtual List<HuntSpecialReqRequest> HuntSpecialReqs { get; set; }
        public virtual List<PlayerRequest> Players { get; set; }
    }
}
