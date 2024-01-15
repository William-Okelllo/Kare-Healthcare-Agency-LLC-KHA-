using System;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Email Address ")]
        public String Email_Address { get; set; }
    }
}
