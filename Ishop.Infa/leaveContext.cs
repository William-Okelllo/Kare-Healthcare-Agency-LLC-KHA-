using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class leaveContext : DbContext
    {
        public leaveContext()
           : base("Planning")
        {
        }
        public DbSet<leave> leave { get; set; }
    }
}