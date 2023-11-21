using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Template_Context : DbContext
    {
        public Template_Context()
           : base("Planning")
        {
        }
        public DbSet<Template> templates { get; set; }
    }
}