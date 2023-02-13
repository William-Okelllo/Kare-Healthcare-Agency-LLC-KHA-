using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ileave
    {
        void Add(leave leave);
        void Edit(leave leave);
        void Remove(int Id);

        IEnumerable Getleaves(); leave FindById(int Id);


    }
}
