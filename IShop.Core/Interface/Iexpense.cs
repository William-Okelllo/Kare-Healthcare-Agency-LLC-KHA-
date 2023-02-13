using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iexpense
    {
        void Add(Expense expense);
        void Edit(Expense expense);
        void Remove(int Id);

        IEnumerable GetExpense(); Expense FindById(int Id);


    }
}
