using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Request
{
    public partial class ImbuementLevelRequest
    {
        public ImbuementLevelRequest()
        {
            HuntImbuements = new HashSet<HuntImbuementRequest>();
            ImbuementDescs = new HashSet<ImbuementDescRequest>();
            ImbuementItems = new HashSet<ImbuementItemRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuementRequest> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDescRequest> ImbuementDescs { get; set; }
        public virtual ICollection<ImbuementItemRequest> ImbuementItems { get; set; }
    }
}
