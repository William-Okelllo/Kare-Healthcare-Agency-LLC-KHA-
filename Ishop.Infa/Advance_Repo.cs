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
    public class Advance_Repo : Iadvance
    {
        Advance_Context context = new Advance_Context();

        public void Add(Advance advance)
        {
            context.Advances.Add(advance);
            context.SaveChanges();
        }

        public void Edit(Advance advance)
        {
            context.Entry(advance).State = System.Data.Entity.EntityState.Modified;
        }

        public Advance FindById(int Id)
        {
            var result = (from r in context.Advances where r.id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Getadvance()

        { return context.Advances; }

        public void Remove(int Id)

        {
            Advance d = context.Advances.Find(Id);
            context.Advances.Remove(d); context.SaveChanges();

        }
    }

}
