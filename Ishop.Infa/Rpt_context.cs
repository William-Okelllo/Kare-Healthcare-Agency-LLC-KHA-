using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Rpt_context : DbContext
    {
        public Rpt_context()
           : base("Kare")
        {
        }
        public DbSet<Rpt> rpts { get; set; }
    }
}