using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Facilities
    {
        public int Id { get; set; }

        [Required]
        public  String Facility { get; set; }

        public DateTime CreatedOn { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String Facility_Type { get; set; }

        [Display(Name = "Setup By ")]
        public String Owner { get; set; }
        [Required]
        public String Phone { get; set; }
    }
}
