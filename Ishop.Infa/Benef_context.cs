using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Benef_context : DbContext
    {
        public Benef_context()
           : base("Kare")
        {
        }
        public DbSet<Benef> benefs { get; set; }
    }
}