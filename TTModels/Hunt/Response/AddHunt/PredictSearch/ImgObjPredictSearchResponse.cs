using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaModels.Utils;

namespace TomodaTibiaModels.Hunt.Response.AddHunt.PredictSearch
{
    public class ImgObjPredictSearchResponse
    {
        public int Id { get; set; }

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
