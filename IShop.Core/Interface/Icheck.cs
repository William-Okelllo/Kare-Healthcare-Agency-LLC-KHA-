using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Icheck
    {
        void Add(CheckIn checkIn);
        void Edit(CheckIn checkIn);
        void Remove(int Id);

        IEnumerable GetCheckIn(); CheckIn FindById(int Id);


    }
}
