﻿using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ishop.Infa
{
    public class ReportContext : DbContext
    {
        public ReportContext()
           : base("Planning")
        {
        }
        public DbSet<Reports> Reports { get; set; }

    }
}