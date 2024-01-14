using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Timesheet
    {
        public int Id { get; set; }

        [Display(Name = "Direct Hours")]
        public  Decimal Direct_Hours { get; set; }

        [Display(Name = "Indirect Hours")]
        public Decimal InDirect_Hours { get; set; }

        public DateTime CreatedOn { get; set; }


        [Display(Name = "From Date")]
        public DateTime From_Date { get; set; }

        [Display(Name = "End Date")]
        public DateTime End_Date { get; set; }

        [Display(Name = "Staff Name")]
        public string Owner { get; set; }
        public string Department { get; set; }
        public int Status { get; set; }
        
        [Display(Name = "Total Hours")]
        public int Tt { get; set; }








    }
}
