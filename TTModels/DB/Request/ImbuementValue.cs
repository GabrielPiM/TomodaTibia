using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class ImbuementValue
    {
        public int Id { get; set; }
        public int IdImbuementLevel { get; set; }
        public int IdImbuementType { get; set; }
        public string Value { get; set; }

        public virtual ImbuementLevel IdImbuementLevelNavigation { get; set; }
        public virtual ImbuementType IdImbuementTypeNavigation { get; set; }
    }
}
