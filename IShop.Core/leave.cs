using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class leave
    {

        public int Id { get; set; }


        [Display(Name = "Request Date ")]
        public DateTime CreatedOn { get; set; }


        

        public string Department { get; set; }

        public string Designation { get; set; }
        public string Employee { get; set; }

        public string Status { get; set; }

        


        public string Message { get; set; }
        public string phone { get; set; }




        public string Approver_Remarks { get; set; }




        [Display(Name = "HOD Email")]
        public string HR_Email { get; set; }


        public string Emp_Mail { get; set; }



    }
}
