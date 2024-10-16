using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IEmp
    {
        void Add(Emp emp);
        void Edit(Emp emp);
        void Remove(int Id);
        IEnumerable GetEmp(); Emp FindById(int Id);
    }
}