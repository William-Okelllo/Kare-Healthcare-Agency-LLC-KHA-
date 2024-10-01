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
    public class Exams_repo : Iexam
    {
        Exams_context context = new Exams_context();

        public void Add(Exam exam)
        {
            context.exams.Add(exam);
            context.SaveChanges();
        }

        public void Edit(Exam exam)
        {
            context.Entry(exam).State = System.Data.Entity.EntityState.Modified;
        }

        public Exam FindById(int Id)
        {
            var result = (from r in context.exams where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetExam()

        { return context.exams; }

        public void Remove(int Id)

        {
            Exam d = context.exams.Find(Id);
            context.exams.Remove(d); context.SaveChanges();

        }
    }
}
