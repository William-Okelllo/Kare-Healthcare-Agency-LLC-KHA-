using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Activities
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public int TimesheetId { get; set; }
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Project Name")]
        public string Project_Name { get; set; }

        public string Indirect { get; set; }

        [Display(Name = "Hours")]
        public int Indirect_Hours { get; set; }
        public string Day { get; set; }
        public string User { get; set; }
        [Display(Name = "Project Phase")]
        public string Name { get; set; }
        public string Comments { get; set; }
        public decimal Charge { get; set; }
    }
}
