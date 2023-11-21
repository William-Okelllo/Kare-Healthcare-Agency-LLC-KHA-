using System.Collections;

namespace IShop.Core.Interface
{
    public interface Itimesheet
    {
        void Add(Timesheet timesheet);
        void Edit(Timesheet timesheet);
        void Remove(int Id);
        IEnumerable GetTimesheet(); Timesheet FindById(int Id);
    }
}
