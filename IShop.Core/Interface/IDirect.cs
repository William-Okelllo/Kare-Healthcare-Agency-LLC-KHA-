using System.Collections;

namespace IShop.Core.Interface
{
    public interface IDirect
    {
        void Add(Direct direct);
        void Edit(Direct direct);
        void Remove(int Id);
        IEnumerable GetDirect(); Direct FindById(int Id);
    }
}
