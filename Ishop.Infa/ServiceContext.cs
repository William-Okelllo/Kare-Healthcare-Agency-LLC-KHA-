using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ServiceContext : DbContext
    {
        public ServiceContext()
           : base("GRS")
        {
        }
        public DbSet<Service> services { get; set; }
    }
}