using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Timesheet
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [Display(Name = "Staff Name")]
        public string Owner { get; set; }
        public int Status { get; set; }
        public int Weekid { get; set; }
       
        [Display(Name = "Total Hours")]
        public int Tt { get; set; }








    }
}
