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
        public int Sun { get; set; }
        public int Mon { get; set; }
        public int Tue { get; set; }
        public int Wen { get; set; }
        public int Thur { get; set; }
        public int Fri { get; set; }
        public int Sat { get; set; }
        [Display(Name = "Total Hours")]
        public int Tt { get; set; }








    }
}
