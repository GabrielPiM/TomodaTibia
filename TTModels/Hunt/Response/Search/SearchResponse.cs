using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaAPI.Utils;
using TomodaTibiaAPI.Utils.Pagination;
using TomodaTibiaModels.Character.Response;


namespace TomodaTibiaModels.Hunt.Response.Search
{
    public class SearchResponse
    {
        public CharacterResponse Character { get; set; }
        public List<HuntCardResponse> Hunts { get; set; }
    }
}
