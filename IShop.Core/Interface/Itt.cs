using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Itt
    {
        void Add(TT tt);
        void Edit(TT tt);
        void Remove(int Id);

        IEnumerable GetTT(); TT FindById(int Id);


    }
}
