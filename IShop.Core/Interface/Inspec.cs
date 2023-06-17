using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Inspec
    {
        void Add(Inspection inspection);
        void Edit(Inspection inspection);
        void Remove(int Id);
        IEnumerable GetInspection(); Inspection FindById(int Id);


    }

}
