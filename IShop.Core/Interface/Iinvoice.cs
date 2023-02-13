using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iinvoice
    {
        void Add(Invoice invoice);
        void Edit(Invoice invoice);
        void Remove(int Id);

        IEnumerable GetInvoice(); Invoice FindById(int Id);


    }
}
