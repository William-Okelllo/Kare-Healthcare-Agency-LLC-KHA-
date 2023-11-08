using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class ReportGroup
    {
        public int id { get; set; }
        public string Description { get; set; }

        public string Type { get; set; }

        public int Menu_order { get; set; }
    }
}
