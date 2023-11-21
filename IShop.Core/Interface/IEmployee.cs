using System.Collections;

namespace IShop.Core.Interface
{
    public interface IEmployee
    {
        void Add(Employee employee);
        void Edit(Employee employee);
        void Remove(int Id);
        IEnumerable GetEmployee(); Employee FindById(int Id);
    }
}
