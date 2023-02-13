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
    internal class leaveRepo : Ileave
    {
        leaveContext context = new leaveContext();

        public void Add(leave leave)
        {
            context.leave.Add(leave);
            context.SaveChanges();
        }

        public void Edit(leave leave)
        {
            context.Entry(leave).State = System.Data.Entity.EntityState.Modified;
        }

        public leave FindById(int Id)
        {
            var result = (from r in context.leave where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable Getleaves()

        { return context.leave; }

        public void Remove(int Id)

        {
            leave d = context.leave.Find(Id);
            context.leave.Remove(d); context.SaveChanges();

        }
    }
}
