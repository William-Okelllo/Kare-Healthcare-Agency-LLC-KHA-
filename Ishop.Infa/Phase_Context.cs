using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Phase_Context : DbContext
    {
        public Phase_Context()
           : base("Planning")
        {
        }
        public DbSet<Phase> phases { get; set; }
    }
}