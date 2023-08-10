using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ipeople
    {
        void Add(People people);
        void Edit(People people);
        void Remove(int Id);
        IEnumerable GetPeople(); People FindById(int Id);


    }
}