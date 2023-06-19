using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Makes_context : DbContext
    {
        public Makes_context()
           : base("GRS")
        {
        }
        public DbSet<Makes> makes { get; set; }
    }
}