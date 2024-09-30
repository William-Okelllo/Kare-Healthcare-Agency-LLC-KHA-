using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Workplan_context : DbContext
    {
        public Workplan_context()
           : base("Kare")
        {
        }
        public DbSet<Workplan> workplans { get; set; }
    }
}