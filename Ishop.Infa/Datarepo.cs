using IShop.Core;
using IShop.Core.Interface;
using System.Collections;
using System.Linq;

namespace Ishop.Infa
{
    public class Datarepo : Idata
    {
        DataContext context = new DataContext();

        public void Add(Data d)
        {
            context.D.Add(d);
            context.SaveChanges();
        }

        public void Edit(Data d)
        {
            context.Entry(d).State = System.Data.Entity.EntityState.Modified;
        }

        public Data FindById(int Id)
        {
            var result = (from r in context.D where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetDs()

        { return context.D; }

        public void Remove(int Id)

        {
            Data d = context.D.Find(Id);
            context.D.Remove(d); context.SaveChanges();

        }
    }
}
