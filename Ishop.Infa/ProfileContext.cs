using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ProfileContext : DbContext
    {
        public ProfileContext()
           : base("Job_Villa")
        {
        }
        public DbSet<Profile> profiles { get; set; }
    }
}