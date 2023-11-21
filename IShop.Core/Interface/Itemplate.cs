using System.Collections;

namespace IShop.Core.Interface
{
    public interface Itemplate
    {
        void Add(Template template);
        void Edit(Template template);
        void Remove(int Id);
        IEnumerable GetTemplate(); Template FindById(int Id);
    }
}
