using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Item_context : DbContext
    {
        public Item_context()
           : base("Kare")
        {
        }
        public DbSet<Items> items { get; set; }
    }
}