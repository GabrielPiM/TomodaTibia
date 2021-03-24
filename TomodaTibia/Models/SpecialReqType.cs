using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class SpecialReqType
    {
        public SpecialReqType()
        {
            HuntSpecialReqs = new HashSet<HuntSpecialReq>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntSpecialReq> HuntSpecialReqs { get; set; }
    }
}
