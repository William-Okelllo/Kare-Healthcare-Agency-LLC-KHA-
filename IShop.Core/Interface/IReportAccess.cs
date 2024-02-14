using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface IReportAccess
    {
        void Add(ReportAccess reportAccess);
        void Edit(ReportAccess reportAccess);
        void Remove(int Id);
        IEnumerable GetReportAccess(); ReportAccess FindById(int Id);
    }
}
