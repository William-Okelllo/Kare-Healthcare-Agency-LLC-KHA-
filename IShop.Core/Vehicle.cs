using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Vehicle
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public String Type { get; set; }
    }
}