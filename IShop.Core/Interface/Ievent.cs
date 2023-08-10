using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ievent
    {
        void Add(Events events);
        void Edit(Events events);
        void Remove(int Id);
        IEnumerable GetEvents(); Events FindById(int Id);


    }
}