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
    public class InspectionRepo : Inspec
    {
        Inspection_Context context = new Inspection_Context();

        public void Add(Inspection inspection)
        {
            context.inspections.Add(inspection);
            context.SaveChanges();
        }

        public void Edit(Inspection inspection)
        {
            context.Entry(inspection).State = System.Data.Entity.EntityState.Modified;
        }

        public Inspection FindById(int Id)
        {
            var result = (from r in context.inspections where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetInspection()

        { return context.inspections; }

        public void Remove(int Id)

        {
            Inspection d = context.inspections.Find(Id);
            context.inspections.Remove(d); context.SaveChanges();

        }
    }
}
