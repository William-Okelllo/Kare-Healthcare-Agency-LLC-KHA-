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
    public class InsuaranceRepo : ins
    {
        InsuaranceContext context = new InsuaranceContext();

        public void Add(Insuarance insuarance)
        {
            context.insuarances.Add(insuarance);
            context.SaveChanges();
        }

        public void Edit(Insuarance insuarance)
        {
            context.Entry(insuarance).State = System.Data.Entity.EntityState.Modified;
        }

        public Insuarance FindById(int Id)
        {
            var result = (from r in context.insuarances where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetInsuarance()

        { return context.insuarances; }

        public void Remove(int Id)

        {
            Insuarance d = context.insuarances.Find(Id);
            context.insuarances.Remove(d); context.SaveChanges();

        }
    }
}
