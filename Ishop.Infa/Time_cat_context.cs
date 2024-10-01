using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Time_cat_context : DbContext
    {
        public Time_cat_context()
           : base("Kare")
        {
        }
        public DbSet<Time_cat> time_Cats { get; set; }
    }
}