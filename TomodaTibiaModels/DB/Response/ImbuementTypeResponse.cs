using System;
using System.Collections.Generic;
 

namespace TomodaTibiaModels.DB.Response
{
    public partial class ImbuementTypeResponse
    {
        public ImbuementTypeResponse()
        {
            HuntImbuements = new HashSet<HuntImbuementResponse>();
            ImbuementDescs = new HashSet<ImbuementDescResponse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuementResponse> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDescResponse> ImbuementDescs { get; set; }
    }
}
