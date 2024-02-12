using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ireopen
    {
        void Add(Reopen reopen);
        void Edit(Reopen reopen);
        void Remove(int Id);
        IEnumerable GetReopen(); Reopen FindById(int Id);
    }
}
