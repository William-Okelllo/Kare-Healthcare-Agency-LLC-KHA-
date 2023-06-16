using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Isev

    {
        void Add(Sev sev);
        void Edit(Sev sev);
        void Remove(int Id);

        IEnumerable GetSev(); Sev FindById(int Id);


    }
}
