using IShop.Core;
using IShop.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Holiday_Repo : IHolida
    {
        Holiday_context context = new Holiday_context();

        public void Add(Holiday holiday)
        {
            context.holidays.Add(holiday);
            context.SaveChanges();
        }

        public void Edit(Holiday holiday)
        {
            context.Entry(holiday).State = System.Data.Entity.EntityState.Modified;
        }

        public Holiday FindById(int Id)
        {
            var result = (from r in context.holidays where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Getholiday()

        { return context.holidays; }

        public void Remove(int Id)

        {
            Holiday d = context.holidays.Find(Id);
            context.holidays.Remove(d); context.SaveChanges();

        }
    }
}
