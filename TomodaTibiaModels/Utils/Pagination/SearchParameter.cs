using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomodaTibiaAPI.Utils.Pagination
{
    public class SearchParameter : QueryParameter
    {
        public int IdAuthor { get; set; }
        public int IdClientVersion { get; set; }
        public int LevelMinReq { get; set; }
        public int XpHr { get; set; }
        public int QtyPlayer { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }
        public bool IsPremium { get; set; }
    }
}
