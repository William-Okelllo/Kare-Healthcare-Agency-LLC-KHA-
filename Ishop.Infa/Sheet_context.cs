using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Sheet_context : DbContext
    {
        public Sheet_context()
           : base("Kare")
        {
        }
        public DbSet<Sheet> sheets { get; set; }
    }
}