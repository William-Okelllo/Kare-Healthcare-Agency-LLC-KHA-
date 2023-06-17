using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Inspections_partsContext : DbContext
    {
        public Inspections_partsContext()
           : base("GRS")
        {
        }
        public DbSet<Inspections_parts> inspections_Parts { get; set; }
    }
}