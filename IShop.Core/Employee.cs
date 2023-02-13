using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Employee
    {
        [Display(Name = "Employee ID ")]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Employee_Address { get; set; }
        public string Home_Address { get; set; }
        public string Notes { get; set; }
        public string Emergency_Contact { get; set; }
        
        public DateTime Last_Update { get; set; }

        [Display(Name = "Access Role ")]
        public string Current_Access { get; set; }

        public Decimal Wage { get; set; }



    }
}
