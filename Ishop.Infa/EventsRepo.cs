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
    public class EventsRepo : Ievent
    {
        EventsContext context = new EventsContext();

        public void Add(Events A)
        {
            context.events.Add(A);
            context.SaveChanges();
        }

        public void Edit(Events A)
        {
            context.Entry(A).State = System.Data.Entity.EntityState.Modified;
        }

        public Events FindById(int Id)
        {
            var result = (from r in context.events where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetEvents()

        { return context.events; }
        public void Remove(int Id)

        {
            Events A = context.events.Find(Id);
            context.events.Remove(A); context.SaveChanges();

        }
    }
}
