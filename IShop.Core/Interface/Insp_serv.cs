using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Insp_serv
    {
        void Add(Inspection_serv inspection_Serv);
        void Edit(Inspection_serv inspection_Serv);
        void Remove(int Id);
        IEnumerable GetInspection_serv(); Inspection_serv FindById(int Id);


    }
}
