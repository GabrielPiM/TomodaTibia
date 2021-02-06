using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class PlayerResponse
    {
        public PlayerResponse()
        {
    
        }


        public int Vocation { get; set; }
        public int Level { get; set; }
 
        public virtual EquipamentResponse Equipaments { get; set; }
    }
}
