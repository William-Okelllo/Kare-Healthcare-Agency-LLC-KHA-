using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Waitlist
    {
        public int Id { get; set; }

        public int Student_Id { get; set; }

        [Display(Name = "Application Date")]
        public DateTime Application_Date { get; set; }

        public string  Course  { get; set; }

        public string Status { get; set; }


        [Display(Name = "Addmision Date")]
        public DateTime? Addmision_Date { get; set; }
        public string Comments { get; set; }
    }
}
