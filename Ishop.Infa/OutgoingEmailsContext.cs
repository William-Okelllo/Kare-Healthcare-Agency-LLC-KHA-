using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class OutgoingEmailsContext : DbContext
    {
        public OutgoingEmailsContext()
           : base("Planning")
        {
        }
        public DbSet<OutgoingEmails> outgoingEmails { get; set; }
    }
}