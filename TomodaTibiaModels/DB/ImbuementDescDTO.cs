using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class ImbuementDescDTO
    {
        public int Id { get; set; }
        public int? IdImbuementLevel { get; set; }
        public int? IdImbuementType { get; set; }
        public string Value { get; set; }

        public virtual ImbuementLevelDTO IdImbuementLevelNavigation { get; set; }
        public virtual ImbuementTypeDTO IdImbuementTypeNavigation { get; set; }
    }
}
