using System;
using System.Collections.Generic;

#nullable disable

namespace TomodaTibiaModels.DB.Request
{
    public partial class Prey
    {
        public Prey()
        {
            PlayerPreys = new HashSet<PlayerPreyRequest>();
        }

        public int Id { get; set; }
        public string Img { get; set; }

        public virtual ICollection<PlayerPreyRequest> PlayerPreys { get; set; }
    }
}
