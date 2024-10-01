using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Admission_context : DbContext
    {
        public Admission_context()
           : base("Kare")
        {
        }
        public DbSet<Admission> admissions { get; set; }
    }
}

