using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

