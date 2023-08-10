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
    internal class PeopleRepo : Ipeople
    {
        PeopleContext context = new PeopleContext();

        public void Add(People A)
        {
            context.peoples.Add(A);
            context.SaveChanges();
        }

        public void Edit(People A)
        {
            context.Entry(A).State = System.Data.Entity.EntityState.Modified;
        }

        public People FindById(int Id)
        {
            var result = (from r in context.peoples where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPeople()

        { return context.peoples; }
        public void Remove(int Id)

        {
            People A = context.peoples.Find(Id);
            context.peoples.Remove(A); context.SaveChanges();

        }
    }
}
    

