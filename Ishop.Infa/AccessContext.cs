using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class AccessContext : DbContext
    {
        public AccessContext()
           : base("Planning")
        {
        }
        public DbSet<Access> Acesss { get; set; }
    }
}