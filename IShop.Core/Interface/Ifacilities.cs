using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ifacilities
    {
        void Add(Facilities facilities);
        void Edit(Facilities facilities);
        void Remove(int Id);

        IEnumerable Getfaclities(); Facilities FindById(int Id);


    }
}
