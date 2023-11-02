using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IDirect
    {
        void Add(Direct direct);
        void Edit(Direct direct);
        void Remove(int Id);
        IEnumerable GetDirect(); Direct FindById(int Id);
    }
}
