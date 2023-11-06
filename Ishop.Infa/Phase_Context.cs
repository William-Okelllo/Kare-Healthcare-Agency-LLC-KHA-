using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Phase_Context : DbContext
    {
        public Phase_Context()
           : base("Planning")
        {
        }
        public DbSet<Phase> phases { get; set; }
    }
}