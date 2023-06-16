using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class SevContext : DbContext
    {
        public SevContext()
           : base("GRS")
        {
        }
        public DbSet<Sev> sev { get; set; }
    }
}