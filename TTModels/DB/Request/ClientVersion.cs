using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class ClientVersion
    {
        public ClientVersion()
        {
            HuntClientVersions = new HashSet<HuntClientVersionRequest>();
        }

        public int Id { get; set; }
        public string VersionName { get; set; }

        public virtual ICollection<HuntClientVersionRequest> HuntClientVersions { get; set; }
    }
}
