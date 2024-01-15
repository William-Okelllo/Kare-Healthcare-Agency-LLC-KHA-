using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ihod
    {
        void Add(HOD hOD);
        void Edit(HOD hOD);
        void Remove(int Id);
        IEnumerable GetHod(); HOD FindById(int Id);
    }
}
