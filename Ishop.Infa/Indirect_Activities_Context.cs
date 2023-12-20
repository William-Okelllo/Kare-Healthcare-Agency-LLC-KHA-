using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Indirect_Activities_Context : DbContext
    {
        public Indirect_Activities_Context()
           : base("Planning")
        {
        }
        public DbSet<Indirect_Activities> indirect_Activities { get; set; }
    }
}