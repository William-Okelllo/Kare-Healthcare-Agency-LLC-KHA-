using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class AutoContext : DbContext
    {
        public AutoContext()
           : base("GRS")
        {
        }
        public DbSet<Autoparts> autoparts { get; set; }
    }
}