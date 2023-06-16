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
    public class SevRepo : Isev
    {
        SevContext context = new SevContext();

        public void Add(Sev sev)
        {
            context.sev.Add(sev);
            context.SaveChanges();
        }

        public void Edit(Sev sev)
        {
            context.Entry(sev).State = System.Data.Entity.EntityState.Modified;
        }

        public Sev FindById(int Id)
        {
            var result = (from r in context.sev where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetSev()

        { return context.sev; }

        public void Remove(int Id)

        {
            Sev d = context.sev.Find(Id);
            context.sev.Remove(d); context.SaveChanges();

        }
    }
}
