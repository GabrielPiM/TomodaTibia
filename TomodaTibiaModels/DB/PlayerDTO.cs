using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB
{
    public partial class PlayerDTO
    {
        public PlayerDTO()
        {
    
        }


        public int Vocation { get; set; }
        public int Level { get; set; }
 
        public virtual EquipamentDTO Equipaments { get; set; }
    }
}
