using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Direct_Activities
    {
        public int Id { get; set; }

        public int WeekId { get; set; }
        public int Hours { get; set; }
        public DateTime Day_Date { get; set; }
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Project Name")]
        public string Project_Name { get; set; }

        public string User { get; set; }
        public string Comments { get; set; }
        public decimal Charge { get; set; }
        [Display(Name = "Project Phase")]
        public string Name { get; set; }
    }
}

  