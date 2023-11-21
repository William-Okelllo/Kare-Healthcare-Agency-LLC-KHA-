using IShop.Core;
using IShop.Core.Interface;
using System.Collections;
using System.Linq;

namespace Ishop.Infa
{
    public class Phase_Repo : IPhase
    {
        Phase_Context context = new Phase_Context();

        public void Add(Phase phase)
        {
            context.phases.Add(phase);
            context.SaveChanges();
        }

        public void Edit(Phase phase)
        {
            context.Entry(phase).State = System.Data.Entity.EntityState.Modified;
        }

        public Phase FindById(int Id)
        {
            var result = (from r in context.phases where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPhase()

        { return context.phases; }

        public void Remove(int Id)

        {
            Phase d = context.phases.Find(Id);
            context.phases.Remove(d); context.SaveChanges();

        }
    }
}
