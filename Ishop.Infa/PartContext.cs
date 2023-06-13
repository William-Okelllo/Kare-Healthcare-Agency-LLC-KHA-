using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class PartContext : DbContext
    {
        public PartContext()
           : base("GRS")
        {
        }
        public DbSet<Part> parts { get; set; }
    }
}