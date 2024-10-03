using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IBenef
    {
        void Add(Benef benef);
        void Edit(Benef benef);
        void Remove(int Id);
        IEnumerable GetBenef(); Benef FindById(int Id);


    }
}
