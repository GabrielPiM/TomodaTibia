using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class PlayerResponse
    {
        public PlayerResponse()
        {
            
            Imbuements = new List<ImbuementResponse>();
            Items = new List<ImgObjResponse>();
            Preys = new List<PreyResponse>();
        }

     
        public int Level { get; set; }
        public string Function { get; set; }
        public string Vocation { get; set; }


        public  EquipamentResponse Equipments { get; set; }
        public  List<ImbuementResponse> Imbuements { get; set; }
        public  List<ImgObjResponse> Items { get; set; }
        public  List<PreyResponse> Preys { get; set; }
    }
}
