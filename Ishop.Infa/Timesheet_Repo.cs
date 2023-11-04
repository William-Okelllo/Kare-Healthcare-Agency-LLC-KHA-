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
    public class Timesheet_Repo : Itimesheet
    {
        Timesheet_Context context = new Timesheet_Context();

        public void Add(Timesheet timesheet)
        {
            context.timesheets.Add(timesheet);
            context.SaveChanges();
        }

        public void Edit(Timesheet timesheet)
        {
            context.Entry(timesheet).State = System.Data.Entity.EntityState.Modified;
        }

        public Timesheet FindById(int Id)
        {
            var result = (from r in context.timesheets where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetTimesheet()

        { return context.timesheets; }

        public void Remove(int Id)

        {
            Timesheet d = context.timesheets.Find(Id);
            context.timesheets.Remove(d); context.SaveChanges();

        }
    }
}