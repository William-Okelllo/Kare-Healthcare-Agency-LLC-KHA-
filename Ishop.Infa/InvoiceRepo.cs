using IShop.Core;
using IShop.Core.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    internal class InvoiceRepo : Iinvoice
    {
        InvoiceContext context = new InvoiceContext();

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
