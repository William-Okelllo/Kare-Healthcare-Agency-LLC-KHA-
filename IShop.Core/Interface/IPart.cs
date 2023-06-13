using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IPart
    {
        void Add(Part part);
        void Edit(Part part);
        void Remove(int Id);
        IEnumerable GetPart(); Part FindById(int Id);


    }
}
