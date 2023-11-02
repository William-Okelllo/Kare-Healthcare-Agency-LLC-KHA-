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
    public class InDirect_Repo : Iindirect
    {
        InDirect_Context context = new InDirect_Context();

        public void Add(InDirect inDirect)
        {
            context.InDirects.Add(inDirect);
            context.SaveChanges();
        }

        public void Edit(InDirect inDirect)
        {
            context.Entry(inDirect).State = System.Data.Entity.EntityState.Modified;
        }

        public InDirect FindById(int Id)
        {
            var result = (from r in context.InDirects where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetinDirect()

        { return context.InDirects; }

        public void Remove(int Id)

        {
            InDirect d = context.InDirects.Find(Id);
            context.InDirects.Remove(d); context.SaveChanges();

        }
    }
}
