using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.Utils.Pagination
{
    public class SearchParameterRequest : PagedQueryParameter
    {
        public SearchParameterRequest()
        {
            IdClientVersion = new List<int?>();
            Level = new List<int>();           
            QtyPlayer = new List<int>();
            Difficulty = new List<int>();
      
        }

        public string CharacterName { get; set; }
        public List<int?> Vocation { get; set; }
        public int IdAuthor { get; set; }
        public List<int?> IdClientVersion { get; set; }
        public List<int> Level { get; set; }
        public int XpHr { get; set; }
        public List<int> QtyPlayer { get; set; }
        public List<int> Difficulty { get; set; }
        public int Rating { get; set; }
        public bool IsPremium { get; set; }
        public int IdLoot { get; set; }
        public void ConfiureDefaults()
        {
            // 0 é o valor de filtragem minimo requerido para pesquisar
            // 3 é o id do client global (default)
            Level.Add(0);
            QtyPlayer.Add(0);
            Difficulty.Add(0);  
            IdClientVersion.Add(3);
        }

    }
}
