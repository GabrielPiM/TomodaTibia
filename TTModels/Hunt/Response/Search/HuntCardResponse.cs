﻿using System;
using System.Collections.Generic;
using System.Text;
using TomodaTibiaModels.DB;


namespace TomodaTibiaModels.Hunt.Response.Search
{
    public class HuntCardResponse 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? LevelMinReq { get; set; }
        public List<string> Monsters { get; set; }
        public int Difficulty { get; set; }

    }
}