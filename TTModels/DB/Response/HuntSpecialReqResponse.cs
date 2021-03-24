using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public  class HuntSpecialReqResponse
    {
        public HuntSpecialReqResponse()
        {
            Objects = new List<ImgObjResponse>();
        }

        public int Name { get; set; }
        public int Type { get; set; }
        
        List<ImgObjResponse> Objects { get; set; }

    }
    
}
