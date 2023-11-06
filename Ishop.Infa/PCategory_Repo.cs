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
    public class PCategory_Repo : ICategory
    {
        PCategory_Context context = new PCategory_Context();

        public void Add(Pcategory pcategory)
        {
            context.pcategories.Add(pcategory);
            context.SaveChanges();
        }

        public void Edit(Pcategory pcategory)
        {
            context.Entry(pcategory).State = System.Data.Entity.EntityState.Modified;
        }

        public Pcategory FindById(int Id)
        {
            var result = (from r in context.pcategories where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetPcategory()

        { return context.pcategories; }

        public void Remove(int Id)

        {
            Pcategory d = context.pcategories.Find(Id);
            context.pcategories.Remove(d); context.SaveChanges();

        }
    }
}
