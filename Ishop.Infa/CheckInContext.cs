using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class CheckInContext : DbContext
    {
        public CheckInContext()
           : base("GRS")
        {
        }
        public DbSet<CheckIn> checkIns { get; set; }
    }
}