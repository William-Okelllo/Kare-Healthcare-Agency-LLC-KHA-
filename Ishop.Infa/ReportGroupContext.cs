using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ReportGroupContext : DbContext
    {
        public ReportGroupContext()
           : base("Planning")
        {
        }
        public DbSet<ReportGroup> ReportGroups { get; set; }


    }
}