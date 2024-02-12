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
    public class Time_cat_repo : Itime_cat
    {
        Time_cat_context context = new Time_cat_context();

        public void Add(Time_cat time_Cat)
        {
            context.time_Cats.Add(time_Cat);
            context.SaveChanges();
        }

        public void Edit(Time_cat hOD)
        {
            context.Entry(hOD).State = System.Data.Entity.EntityState.Modified;
        }

        public Time_cat FindById(int Id)
        {
            var result = (from r in context.time_Cats where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetTime_cat()

        { return context.time_Cats; }

        public void Remove(int Id)

        {
            Time_cat d = context.time_Cats.Find(Id);
            context.time_Cats.Remove(d); context.SaveChanges();

        }
    }
}