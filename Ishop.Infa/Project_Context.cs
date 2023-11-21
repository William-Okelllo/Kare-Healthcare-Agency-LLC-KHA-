using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Project_Context : DbContext
    {
        public Project_Context()
           : base("Planning")
        {
        }
        public DbSet<Project> projects { get; set; }
    }
}