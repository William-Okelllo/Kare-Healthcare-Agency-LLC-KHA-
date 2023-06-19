using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Invoice_Context : DbContext
    {
        public Invoice_Context()
           : base("GRS")
        {
        }
        public DbSet<Invoice> invoices { get; set; }
    }
}