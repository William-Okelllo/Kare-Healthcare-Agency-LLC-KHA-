using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Sms_configs_Context : DbContext
    {
        public Sms_configs_Context()
           : base("Kare")
        {
        }
        public DbSet<Sms_configs> sms_Configs { get; set; }
    }
}