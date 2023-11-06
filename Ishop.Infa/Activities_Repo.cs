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
    public class Activities_Repo : IActivities
    {
        Activities_Context context = new Activities_Context();

        public void Add(Activities activities)
        {
            context.activities.Add(activities);
            context.SaveChanges();
        }

        public void Edit(Activities activities)
        {
            context.Entry(activities).State = System.Data.Entity.EntityState.Modified;
        }

        public Activities FindById(int Id)
        {
            var result = (from r in context.activities where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetActivities()

        { return context.activities; }

        public void Remove(int Id)

        {
            Activities d = context.activities.Find(Id);
            context.activities.Remove(d); context.SaveChanges();

        }
    }
}
