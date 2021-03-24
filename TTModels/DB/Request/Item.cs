using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class Item
    {
        public Item()
        {
            ImbuementItems = new HashSet<ImbuementItem>();
            MonsterLoots = new HashSet<MonsterLoot>();
        }

        public int Id { get; set; }
        public string Img { get; set; }

        public virtual ICollection<ImbuementItem> ImbuementItems { get; set; }
        public virtual ICollection<MonsterLoot> MonsterLoots { get; set; }
    }
}
