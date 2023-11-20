using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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