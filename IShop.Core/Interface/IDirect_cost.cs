using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IDirect_cost
    {
        void Add(Direct_cost direct_Cost);
        void Edit(Direct_cost direct_Cost);
        void Remove(int Id);
        IEnumerable GetDirect_cost(); Direct_cost FindById(int Id);
    }
}
