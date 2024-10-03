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
    public class Benef_repo : IBenef
    {
        Benef_context context = new Benef_context();

        public void Add(Benef benef)
        {
            context.benefs.Add(benef);
            context.SaveChanges();
        }

        public void Edit(Benef benef)
        {
            context.Entry(benef).State = System.Data.Entity.EntityState.Modified;
        }

        public Benef FindById(int Id)
        {
            var result = (from r in context.benefs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetBenef()

        { return context.benefs; }

        public void Remove(int Id)

        {
            Benef d = context.benefs.Find(Id);
            context.benefs.Remove(d); context.SaveChanges();

        }
    }
}