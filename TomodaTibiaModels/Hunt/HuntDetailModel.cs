using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TomodaTibiaModels.DB;

namespace TomodaTibiaModels.Hunt
{
   public class HuntDetailModel
    {
        public Equipamento equipamento { get; set; }
        public IEnumerable<ItemModel> item { get; set; }
        public string videoTutorial { get; set; }
        public string descHunt { get; set; }
        public IEnumerable<Player> player { get; set; }
    }
}
