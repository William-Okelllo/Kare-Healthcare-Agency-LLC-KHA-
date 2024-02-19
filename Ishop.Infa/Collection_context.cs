using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Collection_context : DbContext
    {
        public Collection_context()
           : base("Planning")
        {
        }
        public DbSet<Collection> collections { get; set; }
    }
}