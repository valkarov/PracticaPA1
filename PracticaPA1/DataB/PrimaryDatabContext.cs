using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PracticaPA1.Models;

namespace PractivaPA1.DataB
{
    public class PrimaryDatabContext : DbContext
    {
        public PrimaryDatabContext() : base("name=PrimaryDatabContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Vehiculo> Vehiculores { get; set; }
    }
}