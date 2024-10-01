using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IAdmin
    {
        void Add(Admission admission);
        void Edit(Admission admission);
        void Remove(int Id);
        IEnumerable GetAdmission(); Admission FindById(int Id);


    }
}
