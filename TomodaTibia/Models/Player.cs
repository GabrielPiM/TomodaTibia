using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Player
    {
        public Player()
        {
            Equipaments = new HashSet<Equipament>();
            PlayerImbuements = new HashSet<PlayerImbuement>();
            PlayerItems = new HashSet<PlayerItem>();
            PlayerPreys = new HashSet<PlayerPrey>();
        }

        public int Id { get; set; }
        public int IdHunt { get; set; }
        public int Vocation { get; set; }
        public int Level { get; set; }
        public string Function { get; set; }

        public virtual Hunt IdHuntNavigation { get; set; }
        public virtual ICollection<Equipament> Equipaments { get; set; }
        public virtual ICollection<PlayerImbuement> PlayerImbuements { get; set; }
        public virtual ICollection<PlayerItem> PlayerItems { get; set; }
        public virtual ICollection<PlayerPrey> PlayerPreys { get; set; }
    }
}
