using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Staff
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Agency Name ")]
        public String Name { get; set; }

        [Display(Name = "Registered On ")]
        public DateTime CreatedOn { get; set; }
        [Required]
        public String Address { get; set; }

        [Required]
        [Display(Name = "Contact Person ")]
        public String Contact_Person { get; set; }
        [Required]
        public String Phone { get; set; }

        [Required]
        public String Email { get; set; }

       

    }
}
