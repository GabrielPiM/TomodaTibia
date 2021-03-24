using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class ImbuementType
    {
        public ImbuementType()
        {
            ImbuementValues = new HashSet<ImbuementValue>();
            Imbuements = new HashSet<Imbuement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ImbuementValue> ImbuementValues { get; set; }
        public virtual ICollection<Imbuement> Imbuements { get; set; }
    }
}
