
using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class OutgoingsmsContext : DbContext
    {
        public OutgoingsmsContext()
           : base("Planning")
        {
        }
        public DbSet<Outgoingsms> outgoingsms { get; set; }
    }
}