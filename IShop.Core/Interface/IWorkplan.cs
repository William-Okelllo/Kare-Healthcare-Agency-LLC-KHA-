using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IWorkplan
    {
        void Add(Workplan workplan);
        void Edit(Workplan workplan);
        void Remove(int Id);
        IEnumerable GetWorkplan(); Workplan FindById(int Id);
    }
}
