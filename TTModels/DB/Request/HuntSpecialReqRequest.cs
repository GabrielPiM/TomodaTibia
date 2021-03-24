using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class HuntSpecialReqRequest
    {
        public int Id { get; set; }
        public int IdType { get; set; }
        public string Value { get; set; }
        public int Qty { get; set; }

    }
}
