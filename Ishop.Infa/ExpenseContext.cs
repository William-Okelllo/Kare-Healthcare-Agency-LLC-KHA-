using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext()
             : base("Ishop")
        {
        }
        public DbSet<Expense> expenses { get; set; }
    }
}