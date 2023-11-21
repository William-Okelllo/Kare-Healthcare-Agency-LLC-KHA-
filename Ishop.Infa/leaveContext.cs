using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class leaveContext : DbContext
    {
        public leaveContext()
           : base("Planning")
        {
        }
        public DbSet<leave> leave { get; set; }
    }
}