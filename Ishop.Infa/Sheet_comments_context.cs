using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Sheet_comments_context : DbContext
    {
        public Sheet_comments_context()
           : base("Planning")
        {
        }
        public DbSet<Sheet_comments> sheet_Comments { get; set; }
    }
}