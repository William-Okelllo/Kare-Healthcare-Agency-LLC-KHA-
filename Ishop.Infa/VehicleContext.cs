using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
           : base("GRS")
        {
        }
        public DbSet<Vehicle> vehicles { get; set; }
    }
}