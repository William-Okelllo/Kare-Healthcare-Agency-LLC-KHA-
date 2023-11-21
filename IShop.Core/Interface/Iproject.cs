using System.Collections;

namespace IShop.Core.Interface
{
    public interface Iproject
    {
        void Add(Project project);
        void Edit(Project project);
        void Remove(int Id);
        IEnumerable GetProject(); Project FindById(int Id);

    }
}
