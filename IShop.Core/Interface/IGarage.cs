using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IGarage
    {
        void Add(Garage garage);
        void Edit(Garage garage);
        void Remove(int Id);

        IEnumerable GetGarage(); Garage FindById(int Id);


    }
}

