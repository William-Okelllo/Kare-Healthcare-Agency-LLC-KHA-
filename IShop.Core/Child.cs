using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Child
    {
        public int Id { get; set; }
        public int Project_No { get; set; }
        public string Fullnames { get; set; }
        public DateTime DOB { get; set; }
        public DateTime Createdon { get; set; }
        
        public string Email_Address { get; set; }

        public int  PhoneNumber { get; set; }
    }
}
