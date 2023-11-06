using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IActivities
    {
        void Add(Activities activities);
        void Edit(Activities activities);
        void Remove(int Id);
        IEnumerable GetActivities(); Activities FindById(int Id);
    }
}
