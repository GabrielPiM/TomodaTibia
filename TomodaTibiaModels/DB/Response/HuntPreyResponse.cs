using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Response
{
    public partial class HuntPreyResponse
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdPrey { get; set; }
        public int? IdMonster { get; set; }
        public int ReccStar { get; set; }


    }
}
