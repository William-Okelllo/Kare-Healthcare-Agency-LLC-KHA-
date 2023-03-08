using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Recovery_context : DbContext
    {
        public Recovery_context()
             : base("Ishop")
        {
        }
        public DbSet<Recovery> recoveries { get; set; }
    }
}
