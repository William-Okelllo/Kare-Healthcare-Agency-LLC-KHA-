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
    public class Item_repo : Iitem
    {
        Item_context context = new Item_context();

        public void Add(Items items)
        {
            context.items.Add(items);
            context.SaveChanges();
        }

        public void Edit(Items items)
        {
            context.Entry(items).State = System.Data.Entity.EntityState.Modified;
        }

        public Items FindById(int Id)
        {
            var result = (from r in context.items where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetItems()

        { return context.items; }

        public void Remove(int Id)

        {
            Items d = context.items.Find(Id);
            context.items.Remove(d); context.SaveChanges();

        }
    }
}
