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
    public class Admission_repo : IAdmin
    {
        Admission_context context = new Admission_context();

        public void Add(Admission admission)
        {
            context.admissions.Add(admission);
            context.SaveChanges();
        }

        public void Edit(Admission admission)
        {
            context.Entry(admission).State = System.Data.Entity.EntityState.Modified;
        }

        public Admission FindById(int Id)
        {
            var result = (from r in context.admissions where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetAdmission()

        { return context.admissions; }

        public void Remove(int Id)

        {
            Admission d = context.admissions.Find(Id);
            context.admissions.Remove(d); context.SaveChanges();

        }
    }
}