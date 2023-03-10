using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iadvance
    {
        void Add(Advance advance);
        void Edit(Advance advance);
        void Remove(int Id);

        IEnumerable Getadvance(); Advance FindById(int Id);


    }
}
