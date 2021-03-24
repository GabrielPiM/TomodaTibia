using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Response
{
    public partial class StatisticResponse
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int QtyHunt { get; set; }
        public int QtyAuthor { get; set; }
        public int QtyLocation { get; set; }
        public int QtySpell { get; set; }
        public int QtyNpc { get; set; }
        public int QtyHuntingPlace { get; set; }
        public int QtyQuest { get; set; }
        public int QtyAchivement { get; set; }
        public int QtyKey { get; set; }
        public int QtyMount { get; set; }
        public int QtyObject { get; set; }
        public int QtyBook { get; set; }
        public int QtyImbuement { get; set; }
        public int QtyPrey { get; set; }
        public int QtyMonster { get; set; }
        public int QtyMonsterPrey { get; set; }
    }
}
