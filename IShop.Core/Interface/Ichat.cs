using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ichat
    {
        void Add(Chat chat);
        void Edit(Chat chat);
        void Remove(int Id);
        IEnumerable GetChat(); Chat FindById(int Id);


    }
}
