using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Emp
    {
        public int Id { get; set; }

        public int Student_Id { get; set; }

        [Display(Name = "Report Date")]
        public DateTime Report_Date { get; set; }

        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }

        [Display(Name = "End Date")]
        public DateTime End_Date { get; set; }


        [Display(Name = "Employer Name")]
        public string Name { get; set; }

        public string Address { get; set; }

        [Display(Name = "Employment Status")]
        public string Status { get; set; }

        [Display(Name = "Employment Type")]
        public string Type { get; set; }


        [Display(Name = "Employment Contact")]
        public string Contact { get; set; }
    }
}
