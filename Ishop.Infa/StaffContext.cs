using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class StaffContext : DbContext
    {
        public StaffContext()
             : base("Ishop")
        {
        }
        public DbSet<Staff> staffs { get; set; }
    }
}