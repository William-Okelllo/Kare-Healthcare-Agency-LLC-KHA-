using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Istaff
    {
        void Add(Staff staff);
        void Edit(Staff staff);
        void Remove(int Id);

        IEnumerable Getstaffs(); Staff FindById(int Id);


    }
}
