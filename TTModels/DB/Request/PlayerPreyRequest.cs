using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class PlayerPreyRequest
    {
        public int Id { get; set; }
        public int IdPrey { get; set; }
        public int IdMonster { get; set; }
        public int ReccStar { get; set; }

    }
}
