using System;
using System.Collections.Generic;



namespace TomodaTibiaModels.DB.Request
{
    public partial class ClientVersionRequest
    {
        public int Id { get; set; }
        public string VersionName { get; set; }
    }
}
