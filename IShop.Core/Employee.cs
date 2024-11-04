using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Employee
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public string Employement_Status { get; set; }
        public string Education { get; set; }
        public string Ethinicity { get; set; }
        public string Language { get; set; }
        public string City { get; set; }

        public string Zip { get; set; }




        public string Username { get; set; }
        public string Fullname { get; set; }

        public string Email { get; set; }
        public String Contact { get; set; }
        public String DprtName { get; set; }
        public String Designation { get; set; }
        public String Userid { get; set; }
        public bool Active { get; set; }


        [Display(Name = "Charge-Out Rate")]
        public decimal Rate { get; set; }

        

    }
}
