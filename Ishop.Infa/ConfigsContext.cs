using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class ConfigsContext : DbContext
    {
        public ConfigsContext()
           : base("Planning")
        {
        }
        public DbSet<Configs> configs { get; set; }
    }
}