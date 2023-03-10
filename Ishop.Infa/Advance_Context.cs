using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Advance_Context : DbContext
    {
        public Advance_Context()
             : base("Ishop")
        {
        }
        public DbSet<Advance> Advances { get; set; }
    }
}
