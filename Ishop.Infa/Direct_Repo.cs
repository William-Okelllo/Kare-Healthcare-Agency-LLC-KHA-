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
    public class Direct_Repo : IDirect
    {
        Direct_Context context = new Direct_Context();

        public void Add(Direct direct)
        {
            context.directs.Add(direct);
            context.SaveChanges();
        }

        public void Edit(Direct direct)
        {
            context.Entry(direct).State = System.Data.Entity.EntityState.Modified;
        }

        public Direct FindById(int Id)
        {
            var result = (from r in context.directs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetDirect()

        { return context.directs; }

        public void Remove(int Id)

        {
            Direct d = context.directs.Find(Id);
            context.directs.Remove(d); context.SaveChanges();

        }
    }
}
