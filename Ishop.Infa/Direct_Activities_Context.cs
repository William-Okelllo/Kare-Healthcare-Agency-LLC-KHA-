using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Direct_Activities_Context : DbContext
    {
        public Direct_Activities_Context()
           : base("Planning")
        {
        }
        public DbSet<Direct_Activities> direct_Activities { get; set; }
    }
}