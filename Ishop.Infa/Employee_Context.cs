using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Employee_Context : DbContext
    {
        public Employee_Context()
           : base("Kare")
        {
        }
        public DbSet<Employee> employees { get; set; }
    }
}