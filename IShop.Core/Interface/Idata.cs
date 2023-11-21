using System.Collections;

namespace IShop.Core.Interface
{
    public interface Idata
    {
        void Add(Data d);
        void Edit(Data d);
        void Remove(int Id);
        IEnumerable GetDs(); Data FindById(int Id);


    }
}
