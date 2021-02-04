using System;
using System.Collections.Generic;

#nullable disable

namespace EFDataAcessLibrary.Models
{
    public partial class Prey
    {
        public Prey()
        {
            HuntPreys = new HashSet<HuntPrey>();
        }

        public int Id { get; set; }
        public string Img { get; set; }

        public virtual ICollection<HuntPrey> HuntPreys { get; set; }
    }
}
