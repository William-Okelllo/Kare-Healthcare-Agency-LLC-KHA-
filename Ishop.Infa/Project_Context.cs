using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Project_Context : DbContext
    {
        public Project_Context()
           : base("Planning")
        {
        }
        public DbSet<Project> projects { get; set; }
    }
}