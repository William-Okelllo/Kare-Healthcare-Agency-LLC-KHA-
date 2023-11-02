using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Sms_configs_Context : DbContext
    {
        public Sms_configs_Context()
           : base("Planning")
        {
        }
        public DbSet<Sms_configs> sms_Configs { get; set; }
    }
}