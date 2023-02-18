using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class TT_Context : DbContext
    {
        public TT_Context()
             : base("Ishop")
        {
        }
        public DbSet<TT> tt { get; set; }
    }
}