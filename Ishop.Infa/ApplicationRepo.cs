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
    public class ApplicationRepo : Iapp
    {
        ApplicationContext context = new ApplicationContext();

        public void Add(Application application)
        {
            context.applications.Add(application);
            context.SaveChanges();
        }

        public void Edit(Application application)
        {
            context.Entry(application).State = System.Data.Entity.EntityState.Modified;
        }

        public Application FindById(int Id)
        {
            var result = (from r in context.applications where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetApps()

        { return context.applications; }

        public void Remove(int Id)

        {
            Application d = context.applications.Find(Id);
            context.applications.Remove(d); context.SaveChanges();

        }
    }
}
