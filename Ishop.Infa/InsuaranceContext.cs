using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class InsuaranceContext : DbContext
    {
        public InsuaranceContext()
           : base("GRS")
        {
        }
        public DbSet<Insuarance> insuarances { get; set; }
    }
}