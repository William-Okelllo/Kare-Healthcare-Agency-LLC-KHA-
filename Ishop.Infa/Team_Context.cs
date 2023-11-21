using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Team_Context : DbContext
    {
        public Team_Context()
           : base("Planning")
        {
        }
        public DbSet<Team> teams { get; set; }
    }
}