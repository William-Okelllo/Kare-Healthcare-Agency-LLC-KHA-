using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IQuestion
    {
        void Add(Question question);
        void Edit(Question question);
        void Remove(int Id);
        IEnumerable GetQuestion(); Question FindById(int Id);
    }
}