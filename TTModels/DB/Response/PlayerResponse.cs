using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Response
{
    public partial class PlayerResponse
    {
        public PlayerResponse()
        {
            Equipaments = new EquipamentResponse();
        }
             
        public string Vocation { get; set; }
        public int Level { get; set; }
 
        public virtual EquipamentResponse Equipaments { get; set; }
    }
}
