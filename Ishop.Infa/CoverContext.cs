using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class CoverContext : DbContext
    {
        public CoverContext()
           : base("GRS")
        {
        }
        public DbSet<Cover> covers { get; set; }
    }
}