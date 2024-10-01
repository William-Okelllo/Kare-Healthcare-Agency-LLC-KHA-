using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Notification_context : DbContext
    {
        public Notification_context()
           : base("Kare")
        {
        }
        public DbSet<Notification> notifications { get; set; }
    }
}