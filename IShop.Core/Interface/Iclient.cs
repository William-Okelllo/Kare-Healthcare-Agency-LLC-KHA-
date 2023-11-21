using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iclient
    {
        void Add(Client client);
        void Edit(Client client);
        void Remove(int Id);
        IEnumerable GetClient(); Client FindById(int Id);
    }
}
