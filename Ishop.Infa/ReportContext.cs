using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class ReportContext : DbContext
    {
        public ReportContext()
           : base("Kare")
        {
        }
        public DbSet<Reports> Reports { get; set; }

    }
}
