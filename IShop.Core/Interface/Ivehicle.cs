using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ivehicle
    {
        void Add(Vehicle vehicle);
        void Edit(Vehicle vehicle);
        void Remove(int Id);

        IEnumerable GetVehicles(); Vehicle FindById(int Id);


    }
}
