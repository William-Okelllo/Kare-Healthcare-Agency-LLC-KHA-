using IShop.Core.Interface;
using IShop.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Ishop.Infa
{
    public class Indirect_repo : IIndirect
    {
        Indirect_context context = new Indirect_context();

        public void Add(Indirect indirect)
        {
            context.indirects.Add(indirect);
            context.SaveChanges();
        }

        public void Edit(Indirect indirect)
        {
            context.Entry(indirect).State = System.Data.Entity.EntityState.Modified;
        }

        public Indirect FindById(int Id)
        {
            var result = (from r in context.indirects where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetIndirect()

        { return context.indirects; }

        public void Remove(int Id)

        {
            Indirect d = context.indirects.Find(Id);
            context.indirects.Remove(d); context.SaveChanges();

        }
    }
}