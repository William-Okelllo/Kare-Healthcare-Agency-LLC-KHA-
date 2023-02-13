using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Idata
    {
        void Add(Data d);
        void Edit(Data d);
        void Remove(int Id);
        IEnumerable GetDs(); Data FindById(int Id);


    }
}
