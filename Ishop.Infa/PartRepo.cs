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
    public class PartRepo : IPart
    {
        PartContext context = new PartContext();

        public void Add(Part part)
        {
            context.parts.Add(part);
            context.SaveChanges();
        }

        public void Edit(Part part)
        {
            context.Entry(part).State = System.Data.Entity.EntityState.Modified;
        }

        public Part FindById(int Id)
        {
            var result = (from r in context.parts where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPart()

        { return context.parts; }

        public void Remove(int Id)

        {
            Part d = context.parts.Find(Id);
            context.parts.Remove(d); context.SaveChanges();

        }
    }
}
