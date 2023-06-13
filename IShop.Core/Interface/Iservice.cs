using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iservice
    {
        void Add(Service service);
        void Edit(Service service);
        void Remove(int Id);
        IEnumerable GetService(); Service FindById(int Id);


    }
}
