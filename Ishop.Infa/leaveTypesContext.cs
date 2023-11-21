using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class leaveTypesContext : DbContext
    {
        public leaveTypesContext()
             : base("Planning")
        {
        }
        public DbSet<leaves_Types> leaves_Types { get; set; }
    }
}
