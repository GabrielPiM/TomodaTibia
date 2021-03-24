using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class HuntDesc
    {
        public int Id { get; set; }
        public int IdHunt { get; set; }
        public string Title { get; set; }
        public string Paragraph { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
    }
}
