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
    public class Rpt_repo : Irpt
    {
        Rpt_context context = new Rpt_context();

        public void Add(Rpt rpt)
        {
            context.rpts.Add(rpt);
            context.SaveChanges();
        }

        public void Edit(Rpt rpt)
        {
            context.Entry(rpt).State = System.Data.Entity.EntityState.Modified;
        }

        public Rpt FindById(int Id)
        {
            var result = (from r in context.rpts where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetRpt()

        { return context.rpts; }

        public void Remove(int Id)

        {
            Rpt d = context.rpts.Find(Id);
            context.rpts.Remove(d); context.SaveChanges();

        }
    }
}