using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaModels.DB;

namespace TomodaTibiaModels.Hunt
{
    public class HuntCardModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public int? nivelMinRec { get; set; }
        public IEnumerable<string> urlGif { get; set; }
        public Autor autor { get; set; }
   

    }
}
