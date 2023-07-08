using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface ins
    {
        void Add(Insuarance insuarance);
        void Edit(Insuarance insuarance);
        void Remove(int Id);

        IEnumerable GetInsuarance(); Insuarance FindById(int Id);


    }
}
