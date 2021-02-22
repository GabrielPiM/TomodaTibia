using System;
using System.Collections.Generic;
 

namespace TomodaTibiaModels.DB.Request
{
    public partial class ImbuementTypeRequest
    {
        public ImbuementTypeRequest()
        {
            HuntImbuements = new HashSet<HuntImbuementRequest>();
            ImbuementDescs = new HashSet<ImbuementDescRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuementRequest> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDescRequest> ImbuementDescs { get; set; }
    }
}
