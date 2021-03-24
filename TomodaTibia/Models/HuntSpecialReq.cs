using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class HuntSpecialReq
    {
        public int Id { get; set; }
        public int IdHunt { get; set; }
        public int IdType { get; set; }
        public string Value { get; set; }
        public int Qty { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual SpecialReqType IdTypeNavigation { get; set; }
    }
}
