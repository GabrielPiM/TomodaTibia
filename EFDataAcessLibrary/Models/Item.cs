using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class Item
    {
        public Item()
        {
            HuntItems = new HashSet<HuntItem>();
            HuntLoots = new HashSet<HuntLoot>();
            ImbuementItems = new HashSet<ImbuementItem>();
        }

        public int Id { get; set; }
        public string Img { get; set; }

        public virtual ICollection<HuntItem> HuntItems { get; set; }
        public virtual ICollection<HuntLoot> HuntLoots { get; set; }
        public virtual ICollection<ImbuementItem> ImbuementItems { get; set; }
    }
}
