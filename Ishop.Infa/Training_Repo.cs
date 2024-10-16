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
    public class Training_Repo : ITraining
    {
        Training_context context = new Training_context();

        public void Add(Training training)
        {
            context.training.Add(training);
            context.SaveChanges();
        }

        public void Edit(Training training)
        {
            context.Entry(training).State = System.Data.Entity.EntityState.Modified;
        }

        public Training FindById(int Id)
        {
            var result = (from r in context.training where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetTraining()

        { return context.training; }

        public void Remove(int Id)

        {
            Training d = context.training.Find(Id);
            context.training.Remove(d); context.SaveChanges();

        }
    }
}
