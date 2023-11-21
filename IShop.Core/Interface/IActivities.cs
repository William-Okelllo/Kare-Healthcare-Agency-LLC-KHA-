using System.Collections;

namespace IShop.Core.Interface
{
    public interface IActivities
    {
        void Add(Activities activities);
        void Edit(Activities activities);
        void Remove(int Id);
        IEnumerable GetActivities(); Activities FindById(int Id);
    }
}
