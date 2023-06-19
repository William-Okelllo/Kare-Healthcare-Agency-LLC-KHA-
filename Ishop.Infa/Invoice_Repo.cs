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
    public class Invoice_Repo : inv
    {
        Invoice_Context context = new Invoice_Context();

        public void Add(Invoice invoice)
        {
            context.invoices.Add(invoice);
            context.SaveChanges();
        }

        public void Edit(Invoice invoice)
        {
            context.Entry(invoice).State = System.Data.Entity.EntityState.Modified;
        }

        public Invoice FindById(int Id)
        {
            var result = (from r in context.invoices where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetInvoice()

        { return context.invoices; }

        public void Remove(int Id)

        {
            Invoice d = context.invoices.Find(Id);
            context.invoices.Remove(d); context.SaveChanges();

        }
    }
}
