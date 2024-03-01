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
    public class Notification_repo : INotification
    {
        Notification_context context = new Notification_context();

        public void Add(Notification notification)
        {
            context.notifications.Add(notification);
            context.SaveChanges();
        }

        public void Edit(Notification notification)
        {
            context.Entry(notification).State = System.Data.Entity.EntityState.Modified;
        }

        public Notification FindById(int Id)
        {
            var result = (from r in context.notifications where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetNotification()

        { return context.notifications; }

        public void Remove(int Id)

        {
            Notification d = context.notifications.Find(Id);
            context.notifications.Remove(d); context.SaveChanges();

        }
    }
}
