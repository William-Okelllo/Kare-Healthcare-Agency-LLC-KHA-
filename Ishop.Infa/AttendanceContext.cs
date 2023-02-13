using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class AttendanceContext : DbContext
    {
        public AttendanceContext()
           : base("Ishop")
        {
        }
        public DbSet<Attendance> attendances { get; set; }
    }
}