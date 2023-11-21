using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Client
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Name { get; set;}

        public string Address { get; set; }
        public string Country { get; set; }
        public string Email_Address { get; set; }
        public string Phone { get; set; }
    }
}
