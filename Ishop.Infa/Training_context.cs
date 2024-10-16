using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Training_context : DbContext
    {
        public Training_context()
           : base("Kare")
        {
        }
        public DbSet<Training> training { get; set; }
    }
}

