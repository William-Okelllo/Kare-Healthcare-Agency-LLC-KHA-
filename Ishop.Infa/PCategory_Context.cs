﻿using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class PCategory_Context : DbContext
    {
        public PCategory_Context()
           : base("Planning")
        {
        }
        public DbSet<Pcategory> pcategories { get; set; }
    }
}