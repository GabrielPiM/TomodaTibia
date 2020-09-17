using System;
using System.Collections.Generic;

namespace TomodaTibiaModels.DB
{
    public partial class Item
    {
        public Item()
        {
            OutroItem = new HashSet<OutroItem>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<OutroItem> OutroItem { get; set; }
    }
}
