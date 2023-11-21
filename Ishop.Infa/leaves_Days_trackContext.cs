using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class leaves_Days_trackContext : DbContext
    {
        public leaves_Days_trackContext()
             : base("Planning")
        {
        }
        public DbSet<leaves_Days_track> Leaves_Days_Tracks { get; set; }
    }
}