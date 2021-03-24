using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class Imbuement
    {
        public Imbuement()
        {
            ImbuementItems = new HashSet<ImbuementItem>();
            PlayerImbuements = new HashSet<PlayerImbuementRequest>();
        }

        public int Id { get; set; }
        public int IdImbuementType { get; set; }
        public string Category { get; set; }
        public string Desc { get; set; }
        public string Img { get; set; }

        public virtual ImbuementType IdImbuementTypeNavigation { get; set; }
        public virtual ICollection<ImbuementItem> ImbuementItems { get; set; }
        public virtual ICollection<PlayerImbuementRequest> PlayerImbuements { get; set; }
    }
}
