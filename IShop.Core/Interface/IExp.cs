using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IExp
    {
        void Add(Exp exp);
        void Edit(Exp exp);
        void Remove(int Id);
        IEnumerable GetExpt(); Exp FindById(int Id);
    }
}
