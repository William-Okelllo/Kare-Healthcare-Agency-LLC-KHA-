using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Admission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Contacts { get; set; }

        public string Email { get; set; }


        public string Gender { get; set; }

        [Display(Name = "Date of Admission")]
        public DateTime Date { get; set; }


        [Display(Name = "Admission No")]
        public string Admin_No { get; set; }
    }
}
