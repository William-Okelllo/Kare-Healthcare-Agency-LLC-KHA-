using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class leave
    {
       
        public int Id { get; set; }


        [Display(Name = "Request Date ")]
        public DateTime CreatedOn { get; set; }


        [Display(Name = "From Date ")]
        public DateTime From_Date { get; set; }


        [Display(Name = "To Date ")]
        public DateTime To_Date { get; set; }

        [Required]
        [Display(Name = "Return Date ")]
        public DateTime Return_Date { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }
        public string Employee { get; set; }

        public string Status { get; set; }

        [Display(Name = "Leave Type")]
        [Required]
        public string Type { get; set; }


        public string Message { get; set; }
        public string phone { get; set; }




        [Display(Name = "Days ")]
        public int Requested_Days { get; set; }
        public string Approver_Remarks { get; set; }




        [Display(Name = "HOD Email")]
        public string HR_Email { get; set; }


        [Display(Name = "PF NO")]
        public string PF_NO{ get; set; }

    public string Emp_Mail { get; set; }

        

    }
}
