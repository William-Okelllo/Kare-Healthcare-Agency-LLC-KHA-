using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iindirect_act
    {
        void Add(Indirect_Activities indirect_Activities);
        void Edit(Indirect_Activities indirect_Activities);
        void Remove(int Id);
        IEnumerable GetIndirect_Activities(); Indirect_Activities FindById(int Id);
    }
}
