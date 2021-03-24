using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaModels.DB.Response
{
    public class ImgObjResponse
    {
        public string Name
        {
            get
            {
                return Formatting.ExtractName(Img);
            }
        }

        public string Img { get; set; }
    }
}
