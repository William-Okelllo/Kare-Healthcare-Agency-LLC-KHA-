using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Itimesheet
    {
        void Add(Timesheet timesheet);
        void Edit(Timesheet timesheet);
        void Remove(int Id);
        IEnumerable GetTimesheet(); Timesheet FindById(int Id);
    }
}
