using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Confirm
    {
        public int Id { get; set; }

        public int Event_Id { get; set; }

        [Display(Name = "Your Name")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [Display(Name = "Spouse Attending?")]
        public bool Spouse { get; set; }
        public bool Children { get; set; }

        [Display(Name = "No of Children")]
        public int? Children_No { get; set; }

        [Display(Name = "Spouse Name")]
        public string Spouse_Name { get; set; }
        [Display(Name = "Spouse Phone no")]
        public string Spouse_Phone { get; set; }
    }
}
