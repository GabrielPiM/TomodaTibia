using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class LootRarity
    {
        public LootRarity()
        {
            MonsterLoots = new HashSet<MonsterLoot>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MonsterLoot> MonsterLoots { get; set; }
    }
}
