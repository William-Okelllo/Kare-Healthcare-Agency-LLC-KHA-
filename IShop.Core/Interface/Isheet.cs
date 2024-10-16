using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Isheet
    {
        void Add(Sheet sheet);
        void Edit(Sheet sheet);
        void Remove(int Id);
        IEnumerable GetSheet(); Sheet FindById(int Id);


    }
}
