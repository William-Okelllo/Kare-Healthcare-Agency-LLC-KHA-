using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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