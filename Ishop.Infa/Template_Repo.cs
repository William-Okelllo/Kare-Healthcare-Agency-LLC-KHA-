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
    public class Template_Repo : Itemplate
    {
        Template_Context context = new Template_Context();

        public void Add(Template template)
        {
            context.templates.Add(template);
            context.SaveChanges();
        }

        public void Edit(Template template)
        {
            context.Entry(template).State = System.Data.Entity.EntityState.Modified;
        }

        public Template FindById(int Id)
        {
            var result = (from r in context.templates where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetTemplate()

        { return context.templates; }

        public void Remove(int Id)

        {
            Template d = context.templates.Find(Id);
            context.templates.Remove(d); context.SaveChanges();

        }
    }
}
