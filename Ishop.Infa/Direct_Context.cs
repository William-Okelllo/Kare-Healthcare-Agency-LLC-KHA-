using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Direct_Context : DbContext
    {
        public Direct_Context()
           : base("Planning")
        {
        }
        public DbSet<Direct> directs { get; set; }
    }
}