using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iticket
    {
        void Add(Ticket ticket);
        void Edit(Ticket ticket);
        void Remove(int Id);

        IEnumerable Gettickets(); Ticket FindById(int Id);


    }
}
