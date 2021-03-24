using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class HuntResponse
    {
        public HuntResponse()
        {

            Vocations = new List<string>();
            HuntClientVersions = new List<string>();
            HuntDescs = new List<HuntDescResponse>();
            HuntMonsters = new List<HuntMonsterResponse>();
            HuntSpecialReqs = new List<HuntSpecialReqResponse>();
            Players = new List<PlayerResponse>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public int LevelMinReq { get; set; }
        public int XpHr { get; set; }
        public int TeamSize { get; set; }
        public string TutorialVideoUrl { get; set; }
        public int? Difficulty { get; set; }
        public bool? IsPremium { get; set; }
        public int? Rating { get; set; }



        public AuthorResponse Author { get; set; }
        public List<string> Vocations { get; set; }
        public  List<string> HuntClientVersions { get; set; }
        public  List<HuntDescResponse> HuntDescs { get; set; }
        public  List<HuntMonsterResponse> HuntMonsters { get; set; }
        public  List<HuntSpecialReqResponse> HuntSpecialReqs { get; set; }
        public  List<PlayerResponse> Players { get; set; }
    }
}
