using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class benitem_context : DbContext
    {
        public benitem_context()
           : base("Kare")
        {
        }
        public DbSet<Benitem> benitems { get; set; }
    }
}