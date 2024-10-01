using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Indirect_context : DbContext
    {
        public Indirect_context()
           : base("Kare")
        {
        }
        public DbSet<Indirect> indirects { get; set; }
    }
}