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
    public class Child_Repo : Ichild
    {
        Child_Context context = new Child_Context();

        public void Add(Child child)
        {
            context.children.Add(child);
            context.SaveChanges();
        }

        public void Edit(Child child)
        {
            context.Entry(child).State = System.Data.Entity.EntityState.Modified;
        }

        public Child FindById(int Id)
        {
            var result = (from r in context.children where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetChild()

        { return context.children; }

        public void Remove(int Id)

        {
            Child d = context.children.Find(Id);
            context.children.Remove(d); context.SaveChanges();

        }
    }
}
