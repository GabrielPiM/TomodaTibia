using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class PlayerRequest
    {
        public PlayerRequest()
        {
            Equipaments = new List<EquipamentRequest>();
            PlayerImbuements = new List<PlayerImbuementRequest>();
            PlayerItems = new List<PlayerItemRequest>();
            PlayerPreys = new List<PlayerPreyRequest>();
        }

        public int Id { get; set; }
        public int Vocation { get; set; }
        public int Level { get; set; }
        public string Function { get; set; }


        public virtual List<EquipamentRequest> Equipaments { get; set; }
        public virtual List<PlayerImbuementRequest> PlayerImbuements { get; set; }
        public virtual List<PlayerItemRequest> PlayerItems { get; set; }
        public virtual List<PlayerPreyRequest> PlayerPreys { get; set; }
    }
}
