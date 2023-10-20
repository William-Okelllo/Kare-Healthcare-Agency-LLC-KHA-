using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ichild
    {
        void Add(Child child);
        void Edit(Child child);
        void Remove(int Id);
        IEnumerable GetChild(); Child FindById(int Id);
    }
}
