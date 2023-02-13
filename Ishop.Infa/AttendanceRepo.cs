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
    public class AttendanceRepo : IAttendance
    {
        AttendanceContext context = new AttendanceContext();

        public void Add(Attendance attendance)
        {
            context.attendances.Add(attendance);
            context.SaveChanges();
        }

        public void Edit(Attendance attendance)
        {
            context.Entry(attendance).State = System.Data.Entity.EntityState.Modified;
        }

        public Attendance FindById(int Id)
        {
            var result = (from r in context.attendances where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetAttendance()

        { return context.attendances; }

        public void Remove(int Id)

        {
            Attendance d = context.attendances.Find(Id);
            context.attendances.Remove(d); context.SaveChanges();

        }
    }
}
