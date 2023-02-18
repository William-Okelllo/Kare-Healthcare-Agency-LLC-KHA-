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
    public class TT_Repo : Itt
    {
        TT_Context context = new TT_Context();

        public void Add(TT tt)
        {
            context.tt.Add(tt);
            context.SaveChanges();
        }

        public void Edit(TT tt)
        {
            context.Entry(tt).State = System.Data.Entity.EntityState.Modified;
        }

        public TT FindById(int Id)
        {
            var result = (from r in context.tt where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetTT()

        { return context.tt; }

        public void Remove(int Id)

        {
            TT d = context.tt.Find(Id);
            context.tt.Remove(d); context.SaveChanges();

        }
    }
}
