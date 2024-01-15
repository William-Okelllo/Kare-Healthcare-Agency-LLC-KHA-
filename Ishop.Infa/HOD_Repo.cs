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
    public class HOD_Repo : Ihod
    {
        HodContext context = new HodContext();

        public void Add(HOD hOD)
        {
            context.hODs.Add(hOD);
            context.SaveChanges();
        }

        public void Edit(HOD hOD)
        {
            context.Entry(hOD).State = System.Data.Entity.EntityState.Modified;
        }

        public HOD FindById(int Id)
        {
            var result = (from r in context.hODs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetHod()

        { return context.hODs; }

        public void Remove(int Id)

        {
            HOD d = context.hODs.Find(Id);
            context.hODs.Remove(d); context.SaveChanges();

        }
    }
}
