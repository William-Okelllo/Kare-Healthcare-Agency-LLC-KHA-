using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iperfom
    {
        void Add(Perform perform);
        void Edit(Perform perform);
        void Remove(int Id);
        IEnumerable GetPerform(); Perform FindById(int Id);
    }
}
