using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB
{
    public partial class HuntDTO
    {
        public HuntDTO()
        {
            Versions = new List<string>();
            Imbuements = new List<ImbuementDTO>();
            OtherItems = new List<ItemDTO>();
            Preys = new List<PreyDTO>();
            Players = new List<PlayerDTO>();
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


        public List<PlayerDTO> Players { get; set; }
        public List<ItemDTO> OtherItems { get; set; }
        public List<PreyDTO> Preys { get; set; }
        public List<ImbuementDTO> Imbuements { get; set; }           
        public List<string> Versions { get; set; }
    }
}
