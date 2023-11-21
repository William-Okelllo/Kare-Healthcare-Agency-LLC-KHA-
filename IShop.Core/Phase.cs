using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Phase
    {
        public int Id { get; set; }
        public int Project_id { get; set; }
        [Display(Name = "Start Date ")]
        public DateTime Start_Date { get; set; }

        [Display(Name = "End Date ")]
        public DateTime End_Date { get; set; }

        [Display(Name = "Phase ")]
        public string Name { get; set; }

        public decimal Budget { get; set; }
    }
}
