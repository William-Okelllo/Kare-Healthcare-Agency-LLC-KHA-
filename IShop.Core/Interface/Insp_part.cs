using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Insp_part
    {
        void Add(Inspections_parts inspections_Parts);
        void Edit(Inspections_parts inspections_Parts);
        void Remove(int Id);
        IEnumerable GetInspections_parts(); Inspections_parts FindById(int Id);


    }
}
