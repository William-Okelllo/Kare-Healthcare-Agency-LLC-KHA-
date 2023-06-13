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
    public class CardRepo : Icard
    {
        Cardcontext context = new Cardcontext();

        public void Add(Card card)
        {
            context.cards.Add(card);
            context.SaveChanges();
        }

        public void Edit(Card card)
        {
            context.Entry(card).State = System.Data.Entity.EntityState.Modified;
        }

        public Card FindById(int Id)
        {
            var result = (from r in context.cards where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetCard()

        { return context.cards; }

        public void Remove(int Id)

        {
            Card d = context.cards.Find(Id);
            context.cards.Remove(d); context.SaveChanges();

        }
    }
}
