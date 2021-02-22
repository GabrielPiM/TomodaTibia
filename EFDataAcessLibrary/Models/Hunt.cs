using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class Hunt
    {
        public Hunt()
        {
            HuntClientVersions = new HashSet<HuntClientVersion>();
            HuntImbuements = new HashSet<HuntImbuement>();
            HuntItems = new HashSet<HuntItem>();
            HuntLoots = new HashSet<HuntLoot>();
            HuntMonsters = new HashSet<HuntMonster>();
            HuntPreys = new HashSet<HuntPrey>();
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public int? IdAuthor { get; set; }
        public string Name { get; set; }
        public int LevelMinReq { get; set; }
        public int XpHr { get; set; }
        public int QtyPlayer { get; set; }
        public string TutorialVideoUrl { get; set; }
        public string DescHunt { get; set; }
        public int? Difficulty { get; set; }
        public int? Rating { get; set; }
        public bool? IsValid { get; set; }
        public bool? IsPremium { get; set; }

        public virtual Author IdAuthorNavigation { get; set; }
        public virtual ICollection<HuntClientVersion> HuntClientVersions { get; set; }
        public virtual ICollection<HuntImbuement> HuntImbuements { get; set; }
        public virtual ICollection<HuntItem> HuntItems { get; set; }
        public virtual ICollection<HuntLoot> HuntLoots { get; set; }
        public virtual ICollection<HuntMonster> HuntMonsters { get; set; }
        public virtual ICollection<HuntPrey> HuntPreys { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
