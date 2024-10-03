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
    public class Immigration_repo : Imm
    {
        Immigrant_context context = new Immigrant_context();

        public void Add(Immigrant immigrant)
        {
            context.immigrants.Add(immigrant);
            context.SaveChanges();
        }

        public void Edit(Immigrant immigrant)
        {
            context.Entry(immigrant).State = System.Data.Entity.EntityState.Modified;
        }

        public Immigrant FindById(int Id)
        {
            var result = (from r in context.immigrants where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetImmigrant()

        { return context.immigrants; }

        public void Remove(int Id)

        {
            Immigrant d = context.immigrants.Find(Id);
            context.immigrants.Remove(d); context.SaveChanges();

        }
    }
}