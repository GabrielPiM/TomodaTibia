﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class ClientVersion
    {
        public ClientVersion()
        {
            HuntClientVersions = new HashSet<HuntClientVersion>();
        }

        public int Id { get; set; }
        public string VersionName { get; set; }

        public virtual ICollection<HuntClientVersion> HuntClientVersions { get; set; }
    }
}
