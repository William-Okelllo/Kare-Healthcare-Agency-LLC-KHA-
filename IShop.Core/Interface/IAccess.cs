using System.Collections;

namespace IShop.Core.Interface
{
    public interface IAccess
    {
        void Add(Access AI);
        void Edit(Access AI);
        void Remove(int Id);
        IEnumerable GetAcesss(); Access FindById(int Id);
    }
}