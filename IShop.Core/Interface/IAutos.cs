using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IAutos
    {
        void Add(Autoparts autoparts);
        void Edit(Autoparts autoparts);
        void Remove(int Id);

        IEnumerable GetAutoparts(); Autoparts FindById(int Id);
    }
}
