using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IEmployee
    {
        void Add(Employee employee);
        void Edit(Employee employee);
        void Remove(int Id);
        IEnumerable GetEmployee(); Employee FindById(int Id);
    }
}
