using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core.Interface
{
    public interface Itime_cat
    {
        void Add(Time_cat time_Cat);
        void Edit(Time_cat time_Cat);
        void Remove(int Id);
        IEnumerable GetTime_cat(); Time_cat FindById(int Id);
    }
}

