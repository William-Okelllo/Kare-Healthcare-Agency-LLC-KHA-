using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Inspec_serv_context : DbContext
    {
        public Inspec_serv_context()
           : base("GRS")
        {
        }
        public DbSet<Inspection_serv> Inspection_Servs { get; set; }
    }
}