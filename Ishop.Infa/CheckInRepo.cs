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
    public class CheckInRepo : Icheck
    {
        CheckInContext context = new CheckInContext();

        public void Add(CheckIn checkIn)
        {
            context.checkIns.Add(checkIn);
            context.SaveChanges();
        }

        public void Edit(CheckIn checkIn)
        {
            context.Entry(checkIn).State = System.Data.Entity.EntityState.Modified;
        }

        public CheckIn FindById(int Id)
        {
            var result = (from r in context.checkIns where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetCheckIn()

        { return context.checkIns; }

        public void Remove(int Id)

        {
            CheckIn d = context.checkIns.Find(Id);
            context.checkIns.Remove(d); context.SaveChanges();

        }
    }
}
