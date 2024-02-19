using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface ICollect
    {
        void Add(Collection collection);
        void Edit(Collection collection);
        void Remove(int Id);
        IEnumerable GetCollection(); Collection FindById(int Id);
    }
}
