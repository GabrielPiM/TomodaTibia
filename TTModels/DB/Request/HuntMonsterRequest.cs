﻿using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntMonsterRequest
    {
        public int Id { get; set; }
        public int IdMonster { get; set; }
        public int? Qty { get; set; }

    }
}
