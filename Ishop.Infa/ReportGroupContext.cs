using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class ReportGroupContext : DbContext
    {
        public ReportGroupContext()
           : base("Kare")
        {
        }
        public DbSet<ReportGroup> ReportGroups { get; set; }


    }
}