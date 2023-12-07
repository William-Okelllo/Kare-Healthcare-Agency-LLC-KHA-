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
    internal class Idays_Repo : IDays_leave
    {
        IDays_leave_context context = new IDays_leave_context();

        public void Add(Days_leave days_Leave)
        {
            context.days_Leaves.Add(days_Leave);
            context.SaveChanges();
        }

        public void Edit(Days_leave days_Leave)
        {
            context.Entry(days_Leave).State = System.Data.Entity.EntityState.Modified;
        }

        public Days_leave FindById(int Id)
        {
            var result = (from r in context.days_Leaves where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetDays_leave()

        { return context.days_Leaves; }

        public void Remove(int Id)

        {
            Days_leave d = context.days_Leaves.Find(Id);
            context.days_Leaves.Remove(d); context.SaveChanges();

        }
    }
}
