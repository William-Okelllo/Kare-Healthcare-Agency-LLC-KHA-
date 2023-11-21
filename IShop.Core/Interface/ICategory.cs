using System.Collections;

namespace IShop.Core.Interface
{
    public interface ICategory
    {
        void Add(Pcategory pcategory);
        void Edit(Pcategory pcategory);
        void Remove(int Id);
        IEnumerable GetPcategory(); Pcategory FindById(int Id);
    }
}
