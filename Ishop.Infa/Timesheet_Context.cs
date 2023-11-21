using IShop.Core;
using System.Data.Entity;

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