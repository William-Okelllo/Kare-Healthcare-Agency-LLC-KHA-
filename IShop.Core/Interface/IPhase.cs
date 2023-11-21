using System.Collections;

namespace IShop.Core.Interface
{
    public interface IPhase
    {
        void Add(Phase phase);
        void Edit(Phase phase);
        void Remove(int Id);
        IEnumerable GetPhase(); Phase FindById(int Id);
    }
}
