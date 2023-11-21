using IShop.Core;
using IShop.Core.Interface;
using System.Collections;
using System.Linq;

namespace Ishop.Infa
{
    internal class Employee_Repo : IEmployee
    {
        Employee_Context context = new Employee_Context();

        public void Add(Employee employee)
        {
            context.employees.Add(employee);
            context.SaveChanges();
        }

        public void Edit(Employee employee)
        {
            context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
        }

        public Employee FindById(int Id)
        {
            var result = (from r in context.employees where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetEmployee()

        { return context.employees; }

        public void Remove(int Id)

        {
            Employee d = context.employees.Find(Id);
            context.employees.Remove(d); context.SaveChanges();

        }
    }
}
