using IShop.Core.Interface;
using IShop.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Expense_Repo : Iexpense
    {
        Expense_context context = new Expense_context();

        public void Add(Expense expense)
        {
            context.expenses.Add(expense);
            context.SaveChanges();
        }

        public void Edit(Expense expense)
        {
            context.Entry(expense).State = System.Data.Entity.EntityState.Modified;
        }

        public Expense FindById(int Id)
        {
            var result = (from r in context.expenses where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetExpense()

        { return context.expenses; }

        public void Remove(int Id)

        {
            Expense d = context.expenses.Find(Id);
            context.expenses.Remove(d); context.SaveChanges();

        }
    }
}