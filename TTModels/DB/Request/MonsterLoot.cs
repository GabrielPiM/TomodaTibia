using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class MonsterLoot
    {
        public int Id { get; set; }
        public int IdMonster { get; set; }
        public int IdItem { get; set; }
        public int IdLootRarity { get; set; }
        public string Amount { get; set; }

        public virtual Item IdItemNavigation { get; set; }
        public virtual LootRarity IdLootRarityNavigation { get; set; }
        public virtual Monster IdMonsterNavigation { get; set; }
    }
}
