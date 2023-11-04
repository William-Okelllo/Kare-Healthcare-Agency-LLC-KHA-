using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Timesheet_Context : DbContext
    {
        public Timesheet_Context()
           : base("Planning")
        {
        }
        public DbSet<Timesheet> timesheets { get; set; }
    }
}