using IShop.Core;
using IShop.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class AccessRepo : IAccess
    {
        AccessContext context = new AccessContext();

        public void Add(Access A)
        {
            context.Acesss.Add(A);
            context.SaveChanges();
        }

        public void Edit(Access A)
        {
            context.Entry(A).State = System.Data.Entity.EntityState.Modified;
        }

        public Access FindById(int Id)
        {
            var result = (from r in context.Acesss where r.id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetAcesss()

        { return context.Acesss; }
        public void Remove(int Id)

        {
            Access A = context.Acesss.Find(Id);
            context.Acesss.Remove(A); context.SaveChanges();

        }
    }
}