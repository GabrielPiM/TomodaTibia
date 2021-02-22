using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class PlayerRequest
    {  
        public PlayerRequest()
        {
            Equipaments = new List<EquipamentRequest>();
        }

        public int Id { get; set; }
        public int Vocation { get; set; }
        public int Level { get; set; }
 
        public List<EquipamentRequest> Equipaments { get; set; }
    }
}
