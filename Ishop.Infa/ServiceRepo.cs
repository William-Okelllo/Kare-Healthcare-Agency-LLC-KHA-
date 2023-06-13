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
    public class ServiceRepo : Iservice
    {
        ServiceContext context = new ServiceContext();

        public void Add(Service service)
        {
            context.services.Add(service);
            context.SaveChanges();
        }

        public void Edit(Service service)
        {
            context.Entry(service).State = System.Data.Entity.EntityState.Modified;
        }

        public Service FindById(int Id)
        {
            var result = (from r in context.services where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetService()

        { return context.services; }

        public void Remove(int Id)

        {
            Service d = context.services.Find(Id);
            context.services.Remove(d); context.SaveChanges();

        }
    }
}
