﻿using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class HodContext : DbContext
    {
        public HodContext()
           : base("Planning")
        {
        }
        public DbSet<HOD> hODs { get; set; }
    }
}