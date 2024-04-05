using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IHolida
    {
        void Add(Holiday holiday);
        void Edit(Holiday holiday);
        void Remove(int Id);
        IEnumerable Getholiday(); Holiday FindById(int Id);
    }
}

