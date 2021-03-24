using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class ImbuementLevel
    {
        public ImbuementLevel()
        {
            ImbuementItems = new HashSet<ImbuementItem>();
            ImbuementValues = new HashSet<ImbuementValue>();
            PlayerImbuements = new HashSet<PlayerImbuement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ImbuementItem> ImbuementItems { get; set; }
        public virtual ICollection<ImbuementValue> ImbuementValues { get; set; }
        public virtual ICollection<PlayerImbuement> PlayerImbuements { get; set; }
    }
}
