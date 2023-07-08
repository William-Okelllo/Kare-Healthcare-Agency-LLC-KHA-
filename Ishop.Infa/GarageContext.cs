using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class GarageContext : DbContext
    {
        public GarageContext()
           : base("GRS")
        {
        }
        public DbSet<Garage> garages { get; set; }
    }
}
