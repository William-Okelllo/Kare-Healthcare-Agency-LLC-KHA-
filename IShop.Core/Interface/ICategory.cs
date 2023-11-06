using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface ICategory
    {
        void Add(Pcategory pcategory);
        void Edit(Pcategory pcategory);
        void Remove(int Id);
        IEnumerable GetPcategory(); Pcategory FindById(int Id);
    }
}
