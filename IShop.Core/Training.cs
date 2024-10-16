using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Training
    {
        public int Id { get; set; }

        public int Student_Id { get; set; }
        [Display(Name = "Start Date")]
        public DateTime Start_Date { get; set; }

        [Display(Name = "End Date")]
        public DateTime End_Date { get; set; }


        public string  School { get; set; }


        [Display(Name = "Tuition Status")]
        public string Status { get; set; }


        [Display(Name = "Tuition Paid")]
        public bool Paid { get; set; }

        [Display(Name = "Tuition Payment Date")]
        public DateTime Tuition_Pay_Date { get; set; }


        [Display(Name = "Tuition Amount")]
        public Decimal Amount { get; set; }

        [Display(Name = "Certification/License Status ")]
        public string Certification_Status { get; set; }


        [Display(Name = "Certification/License Test results ")]

        public string Certification_Test_Result { get; set; }
    }
}
