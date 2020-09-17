using System;
using System.Collections.Generic;

namespace TomodaTibiaModels.DB
{
    public partial class Equipamento
    {
        public int Id { get; set; }
        public int? IdPlayer { get; set; }
        public string Amulet { get; set; }
        public string Bag { get; set; }
        public string Helmet { get; set; }
        public string Armor { get; set; }
        public string WeaponRight { get; set; }
        public string WeaponLeft { get; set; }
        public string Ring { get; set; }
        public string Legs { get; set; }
        public string Boots { get; set; }
        public string Ammo { get; set; }

        public virtual Player IdPlayerNavigation { get; set; }
    }
}
