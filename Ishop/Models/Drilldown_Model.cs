using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ishop.Models
{
    public class Drilldown_Model
    {
        [Display(Name = "Start Date ")]
        public DateTime Start_Date { get; set; }
        [Display(Name = "End Date ")]
        public DateTime End_Date { get; set; }
        public string User { get; set; }
        public string Phase { get; set; }
        public Decimal Logged { get; set; }

        public int Hours { get; set; }

    }
}