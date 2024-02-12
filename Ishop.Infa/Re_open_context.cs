using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Re_open_context : DbContext
    {
        public Re_open_context()
           : base("Planning")
        {
        }
        public DbSet<Reopen> reopens { get; set; }
    }
}