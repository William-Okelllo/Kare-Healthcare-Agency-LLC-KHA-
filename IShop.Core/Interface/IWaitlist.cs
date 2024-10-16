using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IWaitlist
    {
        void Add(Waitlist waitlist);
        void Edit(Waitlist waitlist);
        void Remove(int Id);
        IEnumerable GetWaitlist(); Waitlist FindById(int Id);
    }
}