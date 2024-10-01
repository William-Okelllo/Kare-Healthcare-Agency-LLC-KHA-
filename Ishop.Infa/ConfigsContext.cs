using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class ConfigsContext : DbContext
    {
        public ConfigsContext()
           : base("Kare")
        {
        }
        public DbSet<Configs> configs { get; set; }
    }
}