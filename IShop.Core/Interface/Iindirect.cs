using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iindirect
    {
        void Add(InDirect inDirect);
        void Edit(InDirect inDirect);
        void Remove(int Id);
        IEnumerable GetinDirect(); InDirect FindById(int Id);
    }
}
