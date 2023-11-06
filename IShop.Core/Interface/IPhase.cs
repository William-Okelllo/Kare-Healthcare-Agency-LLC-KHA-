using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IPhase
    {
        void Add(Phase phase);
        void Edit(Phase phase);
        void Remove(int Id);
        IEnumerable GetPhase(); Phase FindById(int Id);
    }
}
