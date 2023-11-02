using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Template_Context : DbContext
    {
        public Template_Context()
           : base("Planning")
        {
        }
        public DbSet<Template> templates { get; set; }
    }
}