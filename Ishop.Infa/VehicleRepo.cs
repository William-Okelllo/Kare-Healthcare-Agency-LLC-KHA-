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
    public class VehicleRepo : Ivehicle
    {
        VehicleContext context = new VehicleContext();

        public void Add(Vehicle vehicle)
        {
            context.vehicles.Add(vehicle);
            context.SaveChanges();
        }

        public void Edit(Vehicle vehicle)
        {
            context.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
        }

        public Vehicle FindById(int Id)
        {
            var result = (from r in context.vehicles where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetVehicles()

        { return context.vehicles; }

        public void Remove(int Id)

        {
            Vehicle d = context.vehicles.Find(Id);
            context.vehicles.Remove(d); context.SaveChanges();

        }
    }
}
