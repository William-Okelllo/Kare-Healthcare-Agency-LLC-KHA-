using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class leave
    {

        public int Id { get; set; }


        [Display(Name = "Request Date ")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Total Hours")]
        public Decimal Total_Hours { get; set; }
        public Decimal Days { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }
        public string Employee { get; set; }

        public int Status { get; set; }

        


        public string Message { get; set; }

        [Display(Name = "Phone")]
        public string phone { get; set; }




        public string Approver_Remarks { get; set; }

        [Display(Name = "Leave Type")]
        [Required]
        public string Type { get; set; }


        [Display(Name = "HOD Email")]
        public string HR_Email { get; set; }

        [Display(Name = "Employee Email")]
        public string Emp_Mail { get; set; }



        [Display(Name = "Commence on")]
        public DateTime From_Date { get; set; }

        [Display(Name = "Date of Return")]
        public DateTime Return_Date { get; set; }
        [Display(Name = "End Date")]
        public DateTime To_Date { get; set; }



    }
}
