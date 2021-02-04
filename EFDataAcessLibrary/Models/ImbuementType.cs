using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class ImbuementType
    {
        public ImbuementType()
        {
            HuntImbuements = new HashSet<HuntImbuement>();
            ImbuementDescs = new HashSet<ImbuementDesc>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuement> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDesc> ImbuementDescs { get; set; }
    }
}
