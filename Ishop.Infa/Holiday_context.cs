using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Holiday_context : DbContext
    {
        public Holiday_context()
           : base("Planning")
        {
        }
        public DbSet<Holiday> holidays { get; set; }
    }
}