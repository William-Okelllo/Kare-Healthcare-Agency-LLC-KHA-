using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class DataContext : DbContext
    {
        public DataContext()
           : base("Kare")
        {
        }
        public DbSet<Data> D { get; set; }
    }
}