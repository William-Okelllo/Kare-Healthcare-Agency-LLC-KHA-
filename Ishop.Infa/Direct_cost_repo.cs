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
    public class Direct_cost_repo : IDirect_cost
    {
        Direct_cost_context context = new Direct_cost_context();

        public void Add(Direct_cost direct_Cost)
        {
            context.direct_Costs.Add(direct_Cost);
            context.SaveChanges();
        }

        public void Edit(Direct_cost direct_Cost)
        {
            context.Entry(direct_Cost).State = System.Data.Entity.EntityState.Modified;
        }

        public Direct_cost FindById(int Id)
        {
            var result = (from r in context.direct_Costs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetDirect_cost()

        { return context.direct_Costs; }

        public void Remove(int Id)

        {
            Direct_cost d = context.direct_Costs.Find(Id);
            context.direct_Costs.Remove(d); context.SaveChanges();

        }
    }
}
