using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IAttendance
    {
        void Add(Attendance attendance);
        void Edit(Attendance attendance);
        void Remove(int Id);
        IEnumerable GetAttendance(); Attendance FindById(int Id);


    }
}
