using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomodaTibiaAPI.DB.Models
{
    public class Local
    {
        [Key]
        public int id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("nivelRec")]
        public int NivelRec { get; set; }
        [Column("urlGif")]
        public string URLGif { get; set; }


    }
}
