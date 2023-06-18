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
    public class Inspec_serv_Repo : Insp_serv
    {
        Inspec_serv_context context = new Inspec_serv_context();

        public void Add(Inspection_serv inspection_Serv)
        {
            context.Inspection_Servs.Add(inspection_Serv);
            context.SaveChanges();
        }

        public void Edit(Inspection_serv inspection_Serv)
        {
            context.Entry(inspection_Serv).State = System.Data.Entity.EntityState.Modified;
        }

        public Inspection_serv FindById(int Id)
        {
            var result = (from r in context.Inspection_Servs where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetInspection_serv()

        { return context.Inspection_Servs; }

        public void Remove(int Id)

        {
            Inspection_serv d = context.Inspection_Servs.Find(Id);
            context.Inspection_Servs.Remove(d); context.SaveChanges();

        }
    }
}

