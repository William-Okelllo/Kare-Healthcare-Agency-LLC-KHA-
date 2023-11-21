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
    public class Client_Repo : Iclient
    {
        Client_context context = new Client_context();

        public void Add(Client client)
        {
            context.clients.Add(client);
            context.SaveChanges();
        }

        public void Edit(Client client)
        {
            context.Entry(client).State = System.Data.Entity.EntityState.Modified;
        }

        public Client FindById(int Id)
        {
            var result = (from r in context.clients where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetClient()

        { return context.clients; }

        public void Remove(int Id)

        {
            Client d = context.clients.Find(Id);
            context.clients.Remove(d); context.SaveChanges();

        }
    }
}
