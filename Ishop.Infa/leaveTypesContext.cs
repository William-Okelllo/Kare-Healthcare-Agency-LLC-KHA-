using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class leaveTypesContext : DbContext
    {
        public leaveTypesContext()
             : base("Planning")
        {
        }
        public DbSet<leaves_Types> leaves_Types { get; set; }
    }
}
