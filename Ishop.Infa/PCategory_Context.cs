using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class PCategory_Context : DbContext
    {
        public PCategory_Context()
           : base("Planning")
        {
        }
        public DbSet<Pcategory> pcategories { get; set; }
    }
}