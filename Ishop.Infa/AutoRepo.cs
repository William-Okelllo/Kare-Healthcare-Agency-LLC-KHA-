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
    public class AutoRepo : IAutos
    {
        AutoContext context = new AutoContext();

        public void Add(Autoparts autoparts)
        {
            context.autoparts.Add(autoparts);
            context.SaveChanges();
        }

        public void Edit(Autoparts autoparts)
        {
            context.Entry(autoparts).State = System.Data.Entity.EntityState.Modified;
        }

        public Autoparts FindById(int Id)
        {
            var result = (from r in context.autoparts where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetAutoparts()

        { return context.autoparts; }

        public void Remove(int Id)

        {
            Autoparts d = context.autoparts.Find(Id);
            context.autoparts.Remove(d); context.SaveChanges();

        }
    }
}
