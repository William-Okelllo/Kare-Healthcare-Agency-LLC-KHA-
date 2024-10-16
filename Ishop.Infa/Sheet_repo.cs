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
    public class Sheet_repo : Isheet
    {
        Sheet_context context = new Sheet_context();

        public void Add(Sheet sheet)
        {
            context.sheets.Add(sheet);
            context.SaveChanges();
        }

        public void Edit(Sheet sheet)
        {
            context.Entry(sheet).State = System.Data.Entity.EntityState.Modified;
        }

        public Sheet FindById(int Id)
        {
            var result = (from r in context.sheets where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetSheet()

        { return context.sheets; }

        public void Remove(int Id)

        {
            Sheet d = context.sheets.Find(Id);
            context.sheets.Remove(d); context.SaveChanges();

        }
    }
}