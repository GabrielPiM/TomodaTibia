﻿using System;
using System.Collections.Generic;

 

namespace TomodaTibiaModels.DB.Response
{
    public partial class ImbuementLevelResponse
    {
        public ImbuementLevelResponse()
        {
            HuntImbuements = new HashSet<HuntImbuementResponse>();
            ImbuementDescs = new HashSet<ImbuementDescResponse>();
            ImbuementItems = new HashSet<ImbuementItemResponse>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HuntImbuementResponse> HuntImbuements { get; set; }
        public virtual ICollection<ImbuementDescResponse> ImbuementDescs { get; set; }
        public virtual ICollection<ImbuementItemResponse> ImbuementItems { get; set; }
    }
}