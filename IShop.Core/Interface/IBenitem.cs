using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IBenitem
    {
        void Add(Benitem benitem);
        void Edit(Benitem benitem);
        void Remove(int Id);
        IEnumerable GetBenitem(); Benitem FindById(int Id);


    }
}
    