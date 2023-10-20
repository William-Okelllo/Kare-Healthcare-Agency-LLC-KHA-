
using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class OutgoingsmsContext : DbContext
    {
        public OutgoingsmsContext()
           : base("Compassion")
        {
        }
        public DbSet<Outgoingsms> outgoingsms { get; set; }
    }
}