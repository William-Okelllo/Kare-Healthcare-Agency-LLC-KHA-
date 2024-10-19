using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Expense_context : DbContext
    {
        public Expense_context()
           : base("Kare")
        {
        }
        public DbSet<Expense> expenses { get; set; }
    }
}