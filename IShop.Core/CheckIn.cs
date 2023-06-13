using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class CheckIn
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public String Type { get; set; }
        public String Vehicle_Reg { get; set; }
        public String Phone { get; set; }
        public String Description { get; set; }
        public String Customer { get; set; }
        public String staff { get; set; }
    }
}
