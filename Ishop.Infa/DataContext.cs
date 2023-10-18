using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class DataContext : DbContext
    {
        public DataContext()
           : base("Planning")
        {
        }
        public DbSet<Data> D { get; set; }
    }
}