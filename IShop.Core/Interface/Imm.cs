using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Imm
    {
        void Add(Immigrant immigrant);
        void Edit(Immigrant immigrant);
        void Remove(int Id);
        IEnumerable GetImmigrant(); Immigrant FindById(int Id);


    }
}