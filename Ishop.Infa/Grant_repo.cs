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
    public class Grant_repo : IGrant
    {
        Grant_context context = new Grant_context();

        public void Add(Grant grant)
        {
            context.grants.Add(grant);
            context.SaveChanges();
        }

        public void Edit(Grant grant)
        {
            context.Entry(grant).State = System.Data.Entity.EntityState.Modified;
        }

        public Grant FindById(int Id)
        {
            var result = (from r in context.grants where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetGrant()

        { return context.grants; }

        public void Remove(int Id)

        {
            Grant d = context.grants.Find(Id);
            context.grants.Remove(d); context.SaveChanges();

        }
    }
}