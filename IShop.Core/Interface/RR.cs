using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface RR
    {
        void Add(Recovery recovery);
        void Edit(Recovery recovery);
        void Remove(int Id);

        IEnumerable Getrecovery(); Recovery FindById(int Id);


    }
}
