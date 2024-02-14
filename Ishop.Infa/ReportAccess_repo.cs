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
    public class ReportAccess_repo : IReportAccess
    {
        ReportAccess_context context = new ReportAccess_context();

        public void Add(ReportAccess reportAccess)
        {
            context.reportAccesses.Add(reportAccess);
            context.SaveChanges();
        }

        public void Edit(ReportAccess reportAccess)
        {
            context.Entry(reportAccess).State = System.Data.Entity.EntityState.Modified;
        }

        public ReportAccess FindById(int Id)
        {
            var result = (from r in context.reportAccesses where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetReportAccess()

        { return context.reportAccesses; }

        public void Remove(int Id)

        {
            ReportAccess d = context.reportAccesses.Find(Id);
            context.reportAccesses.Remove(d); context.SaveChanges();

        }
    }
}