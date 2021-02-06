using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntPreyRequest
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdPrey { get; set; }
        public int? IdMonster { get; set; }
        public int ReccStar { get; set; }

    }
}
