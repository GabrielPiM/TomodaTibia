using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Item
    {
        public Item()
        {
            ImbuementItems = new HashSet<ImbuementItem>();
            MonsterLoots = new HashSet<MonsterLoot>();
            PlayerItems = new HashSet<PlayerItem>();
        }

        public int Id { get; set; }
        public string Img { get; set; }

        public virtual ICollection<ImbuementItem> ImbuementItems { get; set; }
        public virtual ICollection<MonsterLoot> MonsterLoots { get; set; }
        public virtual ICollection<PlayerItem> PlayerItems { get; set; }
    }
}
