using System.Collections;

namespace IShop.Core.Interface
{
    public interface Iindirect
    {
        void Add(InDirect inDirect);
        void Edit(InDirect inDirect);
        void Remove(int Id);
        IEnumerable GetinDirect(); InDirect FindById(int Id);
    }
}
