﻿using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class Grant_context : DbContext
    {
        public Grant_context()
           : base("Kare")
        {
        }
        public DbSet<Grant> grants { get; set; }
    }
}