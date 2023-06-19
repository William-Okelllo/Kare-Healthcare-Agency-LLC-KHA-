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
    public class MakesRepo : imakes
    {
        Makes_context context = new Makes_context();

        public void Add(Makes makes)
        {
            context.makes.Add(makes);
            context.SaveChanges();
        }

        public void Edit(Makes makes)
        {
            context.Entry(makes).State = System.Data.Entity.EntityState.Modified;
        }

        public Makes FindById(int Id)
        {
            var result = (from r in context.makes where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetMakes()

        { return context.makes; }

        public void Remove(int Id)

        {
            Makes d = context.makes.Find(Id);
            context.makes.Remove(d); context.SaveChanges();

        }
    }
}
