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
    internal class Waitlist_repo : IWaitlist
    {
        Waitlist_context context = new Waitlist_context();

        public void Add(Waitlist waitlist)
        {
            context.waitlists.Add(waitlist);
            context.SaveChanges();
        }

        public void Edit(Waitlist waitlist)
        {
            context.Entry(waitlist).State = System.Data.Entity.EntityState.Modified;
        }

        public Waitlist FindById(int Id)
        {
            var result = (from r in context.waitlists where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetWaitlist()

        { return context.waitlists; }

        public void Remove(int Id)

        {
            Waitlist d = context.waitlists.Find(Id);
            context.waitlists.Remove(d); context.SaveChanges();

        }
    }
}