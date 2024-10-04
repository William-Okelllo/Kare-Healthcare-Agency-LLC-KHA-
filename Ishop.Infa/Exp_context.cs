using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Exp_context : DbContext
    {
        public Exp_context()
           : base("Kare")
        {
        }
        public DbSet<Exp> exps { get; set; }
    }
}