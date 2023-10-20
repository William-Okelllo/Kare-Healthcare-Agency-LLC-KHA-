using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Itemplate
    {
        void Add(Template template);
        void Edit(Template template);
        void Remove(int Id);
        IEnumerable GetTemplate(); Template FindById(int Id);
    }
}
