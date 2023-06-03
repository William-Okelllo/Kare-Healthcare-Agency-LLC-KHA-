using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iprofile
    {
        void Add(Profile profile);
        void Edit(Profile d);
        void Remove(int Id);
        IEnumerable GetProfiles(); Profile FindById(int Id);

    }
}