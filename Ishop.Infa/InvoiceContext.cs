using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext()
             : base("Ishop")
        {
        }
        public DbSet<Invoice> invoices { get; set; }
    }
}