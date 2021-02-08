using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaModels.Character.Response;

namespace TomodaTibiaModels.Hunt.Response
{
    public class SearchResponse
    {
        public CharacterResponse Character { get; set; }
        public List<HuntCardResponse> Hunts { get; set; }
    }
}
