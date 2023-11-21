using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class DataContext : DbContext
    {
        public DataContext()
           : base("Planning")
        {
        }
        public DbSet<Data> D { get; set; }
    }
}