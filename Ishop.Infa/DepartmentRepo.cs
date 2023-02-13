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
   public class DepartmentRepo : Idepartment
    {
        DepartmentContext context = new DepartmentContext();

        public void Add(Department department)
        {
            context.departments.Add(department);
            context.SaveChanges();
        }

        public void Edit(Department department)
        {
            context.Entry(department).State = System.Data.Entity.EntityState.Modified;
        }

        public Department FindById(int Id)
        {
            var result = (from r in context.departments where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetDepartment()

        { return context.departments; }

        public void Remove(int Id)

        {
            Department d = context.departments.Find(Id);
            context.departments.Remove(d); context.SaveChanges();

        }
    }
}

