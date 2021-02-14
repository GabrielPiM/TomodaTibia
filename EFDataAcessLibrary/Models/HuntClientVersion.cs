using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class HuntClientVersion
    {
        public int Id { get; set; }
        public int IdHunt { get; set; }
        public int? IdClientVersion { get; set; }

        public virtual ClientVersion IdClientVersionNavigation { get; set; }
        public virtual Hunt IdHuntNavigation { get; set; }
    }
}
