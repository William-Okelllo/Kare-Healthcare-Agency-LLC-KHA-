using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Department Name ")]
        public String DprtName { get; set; }

        [Required]
        [Display(Name = "Phone Number ")]
        public String Contact { get; set; }

        [Required]
        [Display(Name = "Head Of Department ")]
        public String Manager { get; set; }

        [Required]
        [Display(Name = "Email Address ")]
        public String Email_Address { get; set; }
    }
}
