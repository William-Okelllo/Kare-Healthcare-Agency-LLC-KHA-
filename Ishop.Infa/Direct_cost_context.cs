using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Direct_cost_context : DbContext
    {
        public Direct_cost_context()
           : base("Kare")
        {
        }
        public DbSet<Direct_cost> direct_Costs { get; set; }
    }
}