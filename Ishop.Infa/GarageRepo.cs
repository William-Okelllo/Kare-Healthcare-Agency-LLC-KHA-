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
    public class GarageRepo : IGarage
    {
        GarageContext context = new GarageContext();

        public void Add(Garage garage)
        {
            context.garages.Add(garage);
            context.SaveChanges();
        }

        public void Edit(Garage garage)
        {
            context.Entry(garage).State = System.Data.Entity.EntityState.Modified;
        }

        public Garage FindById(int Id)
        {
            var result = (from r in context.garages where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetGarage()

        { return context.garages; }

        public void Remove(int Id)

        {
            Garage d = context.garages.Find(Id);
            context.garages.Remove(d); context.SaveChanges();

        }
    }
}