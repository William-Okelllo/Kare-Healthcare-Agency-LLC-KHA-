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
    public class Exp_repo : IExp
    {
        Exp_context context = new Exp_context();

        public void Add(Exp exp)
        {
            context.exps.Add(exp);
            context.SaveChanges();
        }

        public void Edit(Exp exp)
        {
            context.Entry(exp).State = System.Data.Entity.EntityState.Modified;
        }

        public Exp FindById(int Id)
        {
            var result = (from r in context.exps where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetExpt()

        { return context.exps; }

        public void Remove(int Id)

        {
            Exp d = context.exps.Find(Id);
            context.exps.Remove(d); context.SaveChanges();

        }
    }
}