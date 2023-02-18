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
    public class TicketRepo : Iticket
    {
        TicketContext context = new TicketContext();

        public void Add(Ticket A)
        {
            context.tickets.Add(A);
            context.SaveChanges();
        }

        public void Edit(Ticket A)
        {
            context.Entry(A).State = System.Data.Entity.EntityState.Modified;
        }

        public Ticket FindById(int Id)
        {
            var result = (from r in context.tickets where r.id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Gettickets()

        { return context.tickets; }
        public void Remove(int Id)

        {
            Ticket A = context.tickets.Find(Id);
            context.tickets.Remove(A); context.SaveChanges();

        }
    }
}