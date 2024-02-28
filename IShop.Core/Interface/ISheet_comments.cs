using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface ISheet_comments
    {
        void Add(Sheet_comments sheet_Comments);
        void Edit(Sheet_comments sheet_Comments);
        void Remove(int Id);
        IEnumerable GetSheet_comments(); Sheet_comments FindById(int Id);
    }
}
