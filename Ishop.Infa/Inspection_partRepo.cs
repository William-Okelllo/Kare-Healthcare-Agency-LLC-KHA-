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
    public class Inspection_partRepo : Insp_part
    {
        Inspections_partsContext context = new Inspections_partsContext();

        public void Add(Inspections_parts inspections_Parts)
        {
            context.inspections_Parts.Add(inspections_Parts);
            context.SaveChanges();
        }

        public void Edit(Inspections_parts inspections_Parts)
        {
            context.Entry(inspections_Parts).State = System.Data.Entity.EntityState.Modified;
        }

        public Inspections_parts FindById(int Id)
        {
            var result = (from r in context.inspections_Parts where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetInspections_parts()

        { return context.inspections_Parts; }

        public void Remove(int Id)

        {
            Inspections_parts d = context.inspections_Parts.Find(Id);
            context.inspections_Parts.Remove(d); context.SaveChanges();

        }
    }
}
