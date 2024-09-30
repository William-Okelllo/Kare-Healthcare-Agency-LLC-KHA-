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
    public class WorkPlan_Repo : IWorkplan
    {
        Workplan_context context = new Workplan_context();

        public void Add(Workplan workplan)
        {
            context.workplans.Add(workplan);
            context.SaveChanges();
        }

        public void Edit(Workplan workplan)
        {
            context.Entry(workplan).State = System.Data.Entity.EntityState.Modified;
        }

        public Workplan FindById(int Id)
        {
            var result = (from r in context.workplans where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetWorkplan()

        { return context.workplans; }

        public void Remove(int Id)

        {
            Workplan d = context.workplans.Find(Id);
            context.workplans.Remove(d); context.SaveChanges();

        }
    }
}
