using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Icontri
    {
        void Add(Contribution contribution);
        void Edit(Contribution contribution);
        void Remove(int Id);
        IEnumerable GetContributions(); Contribution FindById(int Id);
    }
}
