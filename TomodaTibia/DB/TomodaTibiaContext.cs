using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TomodaTibiaAPI.DB.Models;

namespace TomodaTibiaAPI.DB
{
    public class TomodaTibiaContext : DbContext
    {
        public TomodaTibiaContext(DbContextOptions<TomodaTibiaContext> options) : base(options)
        {

        }

        public DbSet<Local> Local { get; set; }

    }
}
