using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ConfirmContext : DbContext
    {
        public ConfirmContext()
           : base("GRS")
        {
        }
        public DbSet<Confirm> confirms { get; set; }
    }
}