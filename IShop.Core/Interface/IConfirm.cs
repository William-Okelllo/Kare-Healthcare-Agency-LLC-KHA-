using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IConfirm
    {
        void Add(Confirm confirm);
        void Edit(Confirm confirm);
        void Remove(int Id);
        IEnumerable GetConfirm(); Confirm FindById(int Id);


    }
}