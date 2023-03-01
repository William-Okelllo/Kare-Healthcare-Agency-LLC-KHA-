using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class TicketContext : DbContext
    {
        public TicketContext()
           : base("Ishop")
        {
        }
        public DbSet<Ticket> tickets { get; set; }

        public System.Data.Entity.DbSet<IShop.Core.leave> leaves { get; set; }
    }
}