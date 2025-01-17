﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class HuntMonster
    {
        public int? IdHunt { get; set; }
        public int? IdMonster { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual Monster IdMonsterNavigation { get; set; }
    }
}
