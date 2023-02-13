using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ChatContext : DbContext
    {
        public ChatContext()
             : base("Ishop")
        {
        }
        public DbSet<Chat> Chats { get; set; }
    }
}