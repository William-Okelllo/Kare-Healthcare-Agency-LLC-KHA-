using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Job_context  : DbContext
    {
        public Job_context()
           : base("Job_Villa")
        {
        }
        public DbSet<Job> Jobs { get; set; }
    }
}