using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface imakes
    {
        void Add(Makes makes);
        void Edit(Makes makes);
        void Remove(int Id);

        IEnumerable GetMakes(); Makes FindById(int Id);


    }
}
