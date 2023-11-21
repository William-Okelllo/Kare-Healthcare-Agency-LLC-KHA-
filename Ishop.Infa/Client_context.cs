using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Client_context : DbContext
    {
        public Client_context()
           : base("Planning")
        {
        }
        public DbSet<Client> clients { get; set; }
    }
}