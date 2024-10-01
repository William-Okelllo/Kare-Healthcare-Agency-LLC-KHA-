using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Perform_context : DbContext
    {
        public Perform_context()
           : base("Kare")
        {
        }
        public DbSet<Perform> performs { get; set; }
    }
}