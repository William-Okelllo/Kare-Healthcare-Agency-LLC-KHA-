using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iitem
    {
        void Add(Items items);
        void Edit(Items items);
        void Remove(int Id);
        IEnumerable GetItems(); Items FindById(int Id);
    }
}