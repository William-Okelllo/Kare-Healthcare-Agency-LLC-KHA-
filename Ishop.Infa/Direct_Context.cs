using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Direct_Context : DbContext
    {
        public Direct_Context()
           : base("Planning")
        {
        }
        public DbSet<Direct> directs { get; set; }
    }
}