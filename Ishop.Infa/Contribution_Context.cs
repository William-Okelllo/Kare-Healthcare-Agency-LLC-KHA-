using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Contribution_Context : DbContext
    {
        public Contribution_Context()
           : base("Compassion")
        {
        }
        public DbSet<Contribution> contributions { get; set; }
    }
}