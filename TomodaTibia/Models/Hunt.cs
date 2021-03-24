using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaAPI.EntityFramework
{
    public partial class Hunt
    {
        public Hunt()
        {
            AuthorFavs = new HashSet<AuthorFav>();
            AuthorRats = new HashSet<AuthorRat>();
            HuntClientVersions = new HashSet<HuntClientVersion>();
            HuntDescs = new HashSet<HuntDesc>();
            HuntMonsters = new HashSet<HuntMonster>();
            HuntSpecialReqs = new HashSet<HuntSpecialReq>();
            Players = new HashSet<Player>();
        }

        public int Id { get; set; }
        public int IdAuthor { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int LevelMinReq { get; set; }
        public int XpHr { get; set; }
        public int TeamSize { get; set; }
        public string TutorialVideoUrl { get; set; }
        public int? Difficulty { get; set; }
        public bool? IsPremium { get; set; }
        public int? Rating { get; set; }
        public int IdSituation { get; set; }

        public virtual Author IdAuthorNavigation { get; set; }
        public virtual Situation IdSituationNavigation { get; set; }
        public virtual ICollection<AuthorFav> AuthorFavs { get; set; }
        public virtual ICollection<AuthorRat> AuthorRats { get; set; }
        public virtual ICollection<HuntClientVersion> HuntClientVersions { get; set; }
        public virtual ICollection<HuntDesc> HuntDescs { get; set; }
        public virtual ICollection<HuntMonster> HuntMonsters { get; set; }
        public virtual ICollection<HuntSpecialReq> HuntSpecialReqs { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
