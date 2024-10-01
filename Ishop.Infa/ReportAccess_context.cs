using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ReportAccess_context : DbContext
    {
        public ReportAccess_context()
           : base("Kare")
        {
        }
        public DbSet<ReportAccess> reportAccesses { get; set; }
    }
}