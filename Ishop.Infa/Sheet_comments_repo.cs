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
    public class Sheet_comments_repo : ISheet_comments
    {
        Sheet_comments_context context = new Sheet_comments_context();

        public void Add(Sheet_comments sheet_Comments)
        {
            context.sheet_Comments.Add(sheet_Comments);
            context.SaveChanges();
        }

        public void Edit(Sheet_comments sheet_Comments)
        {
            context.Entry(sheet_Comments).State = System.Data.Entity.EntityState.Modified;
        }

        public Sheet_comments FindById(int Id)
        {
            var result = (from r in context.sheet_Comments where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetSheet_comments()

        { return context.sheet_Comments; }

        public void Remove(int Id)

        {
            Sheet_comments d = context.sheet_Comments.Find(Id);
            context.sheet_Comments.Remove(d); context.SaveChanges();

        }
    }
}
