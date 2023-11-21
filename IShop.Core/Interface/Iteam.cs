using System.Collections;

namespace IShop.Core.Interface
{
    public interface Iteam
    {
        void Add(Team team);
        void Edit(Team team);
        void Remove(int Id);
        IEnumerable GetTeam(); Team FindById(int Id);
    }
}

