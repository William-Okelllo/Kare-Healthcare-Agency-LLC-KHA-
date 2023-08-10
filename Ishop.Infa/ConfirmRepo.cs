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
    public class ConfirmRepo : IConfirm
    {
        ConfirmContext context = new ConfirmContext();

        public void Add(Confirm A)
        {
            context.confirms.Add(A);
            context.SaveChanges();
        }

        public void Edit(Confirm A)
        {
            context.Entry(A).State = System.Data.Entity.EntityState.Modified;
        }

        public Confirm FindById(int Id)
        {
            var result = (from r in context.confirms where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetConfirm()

        { return context.confirms; }
        public void Remove(int Id)

        {
            Confirm A = context.confirms.Find(Id);
            context.confirms.Remove(A); context.SaveChanges();

        }
    }
}
