using Ishop.Models;
using IShop.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ishop
{
    public class ImgContext : DbContext
    {
        public ImgContext()
           : base("GRS")
        {
        }
        public DbSet<ImageModel> imageModels { get; set; }
    }
}