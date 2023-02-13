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
    public class StaffRepo : Istaff
    {
        StaffContext context = new StaffContext();

        public void Add(Staff staff)
        {
            context.staffs.Add(staff);
            context.SaveChanges();
        }

        public void Edit(Staff staff)
        {
            context.Entry(staff).State = System.Data.Entity.EntityState.Modified;
        }

        public Staff FindById(int Id)
        {
            var result = (from r in context.staffs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Getstaffs()

        { return context.staffs; }

        public void Remove(int Id)

        {
            Staff d = context.staffs.Find(Id);
            context.staffs.Remove(d); context.SaveChanges();

        }
    }
}
