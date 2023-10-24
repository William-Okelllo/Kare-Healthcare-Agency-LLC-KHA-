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
    public class Contribution_Repo : Icontri
    {
        Contribution_Context context = new Contribution_Context();

        public void Add(Contribution contribution)
        {
            context.contributions.Add(contribution);
            context.SaveChanges();
        }

        public void Edit(Contribution contribution)
        {
            context.Entry(contribution).State = System.Data.Entity.EntityState.Modified;
        }

        public Contribution FindById(int Id)
        {
            var result = (from r in context.contributions where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetContributions()

        { return context.contributions; }

        public void Remove(int Id)

        {
            Contribution d = context.contributions.Find(Id);
            context.contributions.Remove(d); context.SaveChanges();

        }
    }
}