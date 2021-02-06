using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntRequest
    {
        public HuntRequest()
        {
            Versions = new List<String>();
            Imbuements = new List<ImbuementRequest>();
            OtherItems = new List<ItemRequest>();
            Preys = new List<PreyRequest>();
            Players = new List<PlayerRequest>();
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


        public List<int> ClientVersions{ get; set; }
        public List<HuntImbuementRequest> Imbuements { get; set; }
        public List<PlayerRequest> Players { get; set; }
        public List<ItemRequest> OtherItems { get; set; }
        public List<PreyRequest> Preys { get; set; }
         
        public List<String> Versions { get; set; }
    }
}
