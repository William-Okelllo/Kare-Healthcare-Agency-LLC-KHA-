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
    public class Re_open_repo : Ireopen
    {
        Re_open_context context = new Re_open_context();

        public void Add(Reopen reopen)
        {
            context.reopens.Add(reopen);
            context.SaveChanges();
        }

        public void Edit(Reopen reopen)
        {
            context.Entry(reopen).State = System.Data.Entity.EntityState.Modified;
        }

        public Reopen FindById(int Id)
        {
            var result = (from r in context.reopens where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetReopen()

        { return context.reopens; }

        public void Remove(int Id)

        {
            Reopen d = context.reopens.Find(Id);
            context.reopens.Remove(d); context.SaveChanges();

        }
    }
}
