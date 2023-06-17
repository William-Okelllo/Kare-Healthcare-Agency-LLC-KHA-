using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Inspection_Context : DbContext
    {
        public Inspection_Context()
           : base("GRS")
        {
        }
        public DbSet<Inspection> inspections { get; set; }
    }
}