using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class SpecialReqType
    {
        public SpecialReqType()
        {
            HuntSpecialReqs = new HashSet<HuntSpecialReqRequest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntSpecialReqRequest> HuntSpecialReqs { get; set; }
    }
}
