using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Child_Context : DbContext
    {
        public Child_Context()
           : base("Compassion")
        {
        }
        public DbSet<Child> children { get; set; }
    }
}