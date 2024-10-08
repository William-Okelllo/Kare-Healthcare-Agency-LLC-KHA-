using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Irpt
    {
        void Add(Rpt rpt);
        void Edit(Rpt rpt);
        void Remove(int Id);
        IEnumerable GetRpt(); Rpt FindById(int Id);


    }
}