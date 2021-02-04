using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB
{
    public partial class ImbuementLevelDTO
    {
        public ImbuementLevelDTO()
        {
            HuntImbuements = new HashSet<HuntImbuementDTO>();
            ImbuementDescs = new HashSet<ImbuementDescDTO>();
            ImbuementItems = new HashSet<ImbuementItemDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuementDTO> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDescDTO> ImbuementDescs { get; set; }
        public virtual ICollection<ImbuementItemDTO> ImbuementItems { get; set; }
    }
}
