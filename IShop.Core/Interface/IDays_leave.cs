using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IDays_leave
    {
        void Add(Days_leave days_Leave);
        void Edit(Days_leave days_Leave);
        void Remove(int Id);
        IEnumerable GetDays_leave(); Days_leave FindById(int Id);
    }
}
