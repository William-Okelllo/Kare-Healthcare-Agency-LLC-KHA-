using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iproject
    {
        void Add(Project project);
        void Edit(Project project);
        void Remove(int Id);
        IEnumerable GetProject(); Project FindById(int Id);
    
    }
}
