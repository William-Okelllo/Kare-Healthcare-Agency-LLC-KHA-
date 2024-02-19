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
    public class Collection_repo : ICollect
    {
        Collection_context context = new Collection_context();

        public void Add(Collection collection)
        {
            context.collections.Add(collection);
            context.SaveChanges();
        }

        public void Edit(Collection collection)
        {
            context.Entry(collection).State = System.Data.Entity.EntityState.Modified;
        }

        public Collection FindById(int Id)
        {
            var result = (from r in context.collections where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetCollection()

        { return context.collections; }

        public void Remove(int Id)

        {
            Collection d = context.collections.Find(Id);
            context.collections.Remove(d); context.SaveChanges();

        }
    }
}
