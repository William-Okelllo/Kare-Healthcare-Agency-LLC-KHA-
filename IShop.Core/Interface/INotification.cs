using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface INotification
    {
        void Add(Notification notification);
        void Edit(Notification notification);
        void Remove(int Id);
        IEnumerable GetNotification(); Notification FindById(int Id);
    }
}

