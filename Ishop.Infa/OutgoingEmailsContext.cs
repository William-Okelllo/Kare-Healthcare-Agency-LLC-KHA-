using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class OutgoingEmailsContext : DbContext
    {
        public OutgoingEmailsContext()
           : base("Compassion")
        {
        }
        public DbSet<OutgoingEmails> outgoingEmails { get; set; }
    }
}