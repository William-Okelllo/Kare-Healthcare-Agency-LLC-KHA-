using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext()
           : base("Ishop")
    {
    }
    public DbSet<Department> departments { get; set; }
}
}