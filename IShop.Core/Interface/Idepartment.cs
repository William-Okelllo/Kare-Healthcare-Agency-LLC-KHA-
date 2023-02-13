using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Idepartment
    {
        void Add(Department department);
        void Edit(Department department);
        void Remove(int Id);
        IEnumerable GetDepartment(); Department FindById(int Id);


    }
}

