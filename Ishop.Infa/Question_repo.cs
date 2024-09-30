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
    public class Question_repo : IQuestion
    {
        Question_context context = new Question_context();

        public void Add(Question question)
        {
            context.questions.Add(question);
            context.SaveChanges();
        }

        public void Edit(Question question)
        {
            context.Entry(question).State = System.Data.Entity.EntityState.Modified;
        }

        public Question FindById(int Id)
        {
            var result = (from r in context.questions where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetQuestion()

        { return context.questions; }

        public void Remove(int Id)

        {
            Question d = context.questions.Find(Id);
            context.questions.Remove(d); context.SaveChanges();

        }
    }
}
