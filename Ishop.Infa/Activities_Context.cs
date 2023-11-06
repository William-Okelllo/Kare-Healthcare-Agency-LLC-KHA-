using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Activities_Context : DbContext
    {
        public Activities_Context()
           : base("Planning")
        {
        }
        public DbSet<Activities> activities { get; set; }
    }
}