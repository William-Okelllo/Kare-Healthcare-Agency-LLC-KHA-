using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ibug
    {
        void Add(Bug bug);
        void Edit(Bug bug);
        void Remove(int Id);

        IEnumerable GetBugs(); Bug FindById(int Id);


    }
}
