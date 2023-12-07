using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class IDays_leave_context : DbContext
    {
        public IDays_leave_context()
           : base("Planning")
        {
        }
        public DbSet<Days_leave> days_Leaves { get; set; }
    }
}