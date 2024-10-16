using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Emp_context : DbContext
    {
        public Emp_context()
           : base("Kare")
        {
        }
        public DbSet<Emp> emps { get; set; }
    }
}