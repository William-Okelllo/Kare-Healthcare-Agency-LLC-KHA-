using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Ijob
    {
        void Add(Job job);
        void Edit(Job job);
        void Remove(int Id);

        IEnumerable Getjobs(); Job FindById(int Id);


    }
}
