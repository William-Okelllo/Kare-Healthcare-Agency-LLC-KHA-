using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class PeopleContext : DbContext
    {
        public PeopleContext()
           : base("GRS")
        {
        }
        public DbSet<People> peoples { get; set; }
    }
}