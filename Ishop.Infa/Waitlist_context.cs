﻿using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Waitlist_context : DbContext
    {
        public Waitlist_context()
           : base("Kare")
        {
        }
        public DbSet<Waitlist> waitlists { get; set; }
    }
}