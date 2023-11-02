using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
   public class AccessContext : DbContext
    {
        public AccessContext()
           : base("Planning")
        {
        }
        public DbSet<Access> Acesss { get; set; }
    }
}