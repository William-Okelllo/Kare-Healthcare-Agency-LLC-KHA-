using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Team_Context : DbContext
    {
        public Team_Context()
           : base("Planning")
        {
        }
        public DbSet<Team> teams { get; set; }
    }
}