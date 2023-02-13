using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Attendance
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Office location")]
        public string Business_location { get; set; }

        [Display(Name = "Office latitude")]
        public string latitude { get; set; }



        [Display(Name = "Office longitude")]
        public string longitude { get; set; }


        [Display(Name = "User latitude")]
        public string user_latitude { get; set; }

        public decimal Distance { get; set; }

        [Display(Name = "User longitude")]
        public string user_longitude { get; set; }

        public string Check_status { get; set; }

        [Display(Name = "User Account")]
        public string User_Account { get; set; }


        [Display(Name = "Device")]
        public string Device { get; set; }
    }
}
