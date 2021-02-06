using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaModels.DB;

namespace TomodaTibiaModels.Hunt
{
    public class HuntCardModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? NivelMinRec { get; set; }
        public IEnumerable<string> URLGif { get; set; }
        public int Difficulty { get; set; }

    }
}
