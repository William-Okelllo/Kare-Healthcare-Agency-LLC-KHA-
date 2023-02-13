using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
   public interface IAccess
    {
        void Add(Access AI);
        void Edit(Access AI);
        void Remove(int Id);
        IEnumerable GetAcesss(); Access FindById(int Id);
    }
}