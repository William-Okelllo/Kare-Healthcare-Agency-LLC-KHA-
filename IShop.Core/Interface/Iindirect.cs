using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IIndirect
    {
        void Add(Indirect indirect);
        void Edit(Indirect indirect);
        void Remove(int Id);
        IEnumerable GetIndirect(); Indirect FindById(int Id);
    }
}
