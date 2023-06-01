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
    public class Job_repo : Ijob
    {
        Job_context context = new Job_context();

        public void Add(Job job)
        {
            context.Jobs.Add(job);
            context.SaveChanges();
        }

        public void Edit(Job job)
        {
            context.Entry(job).State = System.Data.Entity.EntityState.Modified;
        }

        public Job FindById(int Id)
        {
            var result = (from r in context.Jobs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Getjobs()

        { return context.Jobs; }

        public void Remove(int Id)

        {
            Job d = context.Jobs.Find(Id);
            context.Jobs.Remove(d); context.SaveChanges();

        }
    }
}
