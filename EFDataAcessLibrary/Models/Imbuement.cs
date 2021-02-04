using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class Imbuement
    {
        public Imbuement()
        {
            HuntImbuements = new HashSet<HuntImbuement>();
            ImbuementItems = new HashSet<ImbuementItem>();
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }

        public virtual ICollection<HuntImbuement> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementItem> ImbuementItems { get; set; }
    }
}
