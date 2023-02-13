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
    public class ChatRepo : Ichat
    {
        ChatContext context = new ChatContext();

        public void Add(Chat chat)
        {
            context.Chats.Add(chat);
            context.SaveChanges();
        }

        public void Edit(Chat chat)
        {
            context.Entry(chat).State = System.Data.Entity.EntityState.Modified;
        }

        public Chat FindById(int Id)
        {
            var result = (from r in context.Chats where r.Id == Id select r).FirstOrDefault();
            return result;
        }

        public IEnumerable GetChat()

        { return context.Chats; }

        public void Remove(int Id)

        {
            Chat d = context.Chats.Find(Id);
            context.Chats.Remove(d); context.SaveChanges();

        }
    }
}
