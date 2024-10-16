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
    public class Emp_Repo : IEmp
    {
        Emp_context context = new Emp_context();

        public void Add(Emp emp)
        {
            context.emps.Add(emp);
            context.SaveChanges();
        }

        public void Edit(Emp emp)
        {
            context.Entry(emp).State = System.Data.Entity.EntityState.Modified;
        }

        public Emp FindById(int Id)
        {
            var result = (from r in context.emps where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetEmp()

        { return context.emps; }

        public void Remove(int Id)

        {
            Emp d = context.emps.Find(Id);
            context.emps.Remove(d); context.SaveChanges();

        }
    }
}