using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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