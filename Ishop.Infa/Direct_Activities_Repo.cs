using IShop.Core.Interface;
using IShop.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    internal class Direct_Activities_Repo : Idirect_act
    {
        Direct_Activities_Context context = new Direct_Activities_Context();

        public void Add(Direct_Activities direct_Activities)
        {
            context.direct_Activities.Add(direct_Activities);
            context.SaveChanges();
        }

        public void Edit(Direct_Activities direct_Activities)
        {
            context.Entry(direct_Activities).State = System.Data.Entity.EntityState.Modified;
        }

        public Direct_Activities FindById(int Id)
        {
            var result = (from r in context.direct_Activities where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetDirect_Activities()

        { return context.direct_Activities; }

        public void Remove(int Id)

        {
            Direct_Activities d = context.direct_Activities.Find(Id);
            context.direct_Activities.Remove(d); context.SaveChanges();

        }
    }
}
