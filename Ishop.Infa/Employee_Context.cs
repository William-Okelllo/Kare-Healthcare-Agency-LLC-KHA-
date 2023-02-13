using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Employee_Context : DbContext
    {
        public Employee_Context()
           : base("Ishop")
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}