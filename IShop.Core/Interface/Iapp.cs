using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iapp
    {
        void Add(Application application);
        void Edit(Application application);
        void Remove(int Id);

        IEnumerable GetApps(); Application FindById(int Id);


    }
}
