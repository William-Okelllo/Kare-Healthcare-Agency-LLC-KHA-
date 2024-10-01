using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext()
           : base("Kare")
        {
        }
        public DbSet<Department> departments { get; set; }
    }
}