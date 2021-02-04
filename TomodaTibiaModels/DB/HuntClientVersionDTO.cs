using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class HuntClientVersionDTO
    {
        public int Id { get; set; }
        public int? IdHunt { get; set; }
        public int? IdClientVersion { get; set; }

        public virtual ClientVersionDTO IdClientVersionNavigation { get; set; }
        public virtual HuntDTO IdHuntNavigation { get; set; }
    }
}
