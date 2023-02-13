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
    public class Employee_Repo : Iemp
    {
        Employee_Context context = new Employee_Context();

        public void Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void Edit(Employee employee)
        {
            context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        }

        public Employee FindById(int Id)
        {
            var result = (from r in context.Employees where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetEmployee()

        { return context.Employees; }

        public void Remove(int Id)

        {
            Employee d = context.Employees.Find(Id);
            context.Employees.Remove(d); context.SaveChanges();

        }
    }
}
