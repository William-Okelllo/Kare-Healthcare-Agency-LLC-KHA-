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
    public class Perform_Repo : Iperfom
    {
        Perform_context context = new Perform_context();

        public void Add(Perform perform)
        {
            context.performs.Add(perform);
            context.SaveChanges();
        }

        public void Edit(Perform perform)
        {
            context.Entry(perform).State = System.Data.Entity.EntityState.Modified;
        }

        public Perform FindById(int Id)
        {
            var result = (from r in context.performs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPerform()

        { return context.performs; }

        public void Remove(int Id)

        {
            Perform d = context.performs.Find(Id);
            context.performs.Remove(d); context.SaveChanges();

        }
    }
}
