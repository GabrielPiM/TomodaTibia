using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class PlayerRequest
    {
        public PlayerRequest()
        {
    
        }


        public int Vocation { get; set; }
        public int Level { get; set; }
 
        public virtual EquipamentRequest Equipaments { get; set; }
    }
}
