using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Icard
    {
        void Add(Card card);
        void Edit(Card card);
        void Remove(int Id);

        IEnumerable GetCard(); Card FindById(int Id);


    }
}

