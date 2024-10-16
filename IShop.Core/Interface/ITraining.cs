using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface ITraining
    {
        void Add(Training training);
        void Edit(Training training);
        void Remove(int Id);
        IEnumerable GetTraining(); Training FindById(int Id);
    }
}
    
