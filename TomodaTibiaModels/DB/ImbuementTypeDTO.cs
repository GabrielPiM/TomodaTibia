using System;
using System.Collections.Generic;
 

namespace TomodaTibiaModels.DB
{
    public partial class ImbuementTypeDTO
    {
        public ImbuementTypeDTO()
        {
            HuntImbuements = new HashSet<HuntImbuementDTO>();
            ImbuementDescs = new HashSet<ImbuementDescDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuementDTO> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDescDTO> ImbuementDescs { get; set; }
    }
}
