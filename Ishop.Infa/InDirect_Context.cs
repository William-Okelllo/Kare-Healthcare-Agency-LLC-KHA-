using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class InDirect_Context : DbContext
    {
        public InDirect_Context()
           : base("Planning")
        {
        }
        public DbSet<InDirect> InDirects { get; set; }
    }
}