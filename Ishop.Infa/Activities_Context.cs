﻿using IShop.Core;
using System.Data.Entity;

namespace Ishop.Infa
{
    public class Activities_Context : DbContext
    {
        public Activities_Context()
           : base("Kare")
        {
        }
        public DbSet<Activities> activities { get; set; }
    }
}