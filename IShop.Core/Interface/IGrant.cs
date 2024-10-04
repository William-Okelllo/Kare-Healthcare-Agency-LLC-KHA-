using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IGrant
    {
        void Add(Grant grant);
        void Edit(Grant grant);
        void Remove(int Id);
        IEnumerable GetGrant(); Grant FindById(int Id);


    }
}
