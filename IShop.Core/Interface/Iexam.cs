using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Iexam
    {
        void Add(Exam exam);
        void Edit(Exam exam);
        void Remove(int Id);
        IEnumerable GetExam(); Exam FindById(int Id);
    }
}
