using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Idirect_act
    {
        void Add(Direct_Activities direct_Activities);
        void Edit(Direct_Activities direct_Activities);
        void Remove(int Id);
        IEnumerable GetDirect_Activities(); Direct_Activities FindById(int Id);
    
    }
}
