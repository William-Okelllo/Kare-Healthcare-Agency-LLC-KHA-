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
    internal class Recovery_Repo : RR
    {
        Recovery_context context = new Recovery_context();

        public void Add(Recovery recovery)
        {
            context.recoveries.Add(recovery);
            context.SaveChanges();
        }

        public void Edit(Recovery recovery)
        {
            context.Entry(recovery).State = System.Data.Entity.EntityState.Modified;
        }

        public Recovery FindById(int Id)
        {
            var result = (from r in context.recoveries where r.id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Getrecovery()

        { return context.recoveries; }

        public void Remove(int Id)

        {
            Recovery d = context.recoveries.Find(Id);
            context.recoveries.Remove(d); context.SaveChanges();

        }
    }
}
