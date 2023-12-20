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
    public class Indirect_Activities_Repo : Iindirect_act
    {
        Indirect_Activities_Context context = new Indirect_Activities_Context();

        public void Add(Indirect_Activities indirect_Activities)
        {
            context.indirect_Activities.Add(indirect_Activities);
            context.SaveChanges();
        }

        public void Edit(Indirect_Activities indirect_Activities)
        {
            context.Entry(indirect_Activities).State = System.Data.Entity.EntityState.Modified;
        }

        public Indirect_Activities FindById(int Id)
        {
            var result = (from r in context.indirect_Activities where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetIndirect_Activities()

        { return context.indirect_Activities; }

        public void Remove(int Id)

        {
            Indirect_Activities d = context.indirect_Activities.Find(Id);
            context.indirect_Activities.Remove(d); context.SaveChanges();

        }
    }
}
