﻿using System;
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

        public int WeekNo { get; set; }
        public Decimal Hours { get; set; }
        public DateTime Day_Date { get; set; }
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Project Name")]
        public string Project_Name { get; set; }

        [Display(Name = "Time Category")]
        public string Category { get; set; }
        public string User { get; set; }
        public string Comments { get; set; }
        public decimal Charge { get; set; }
        [Display(Name = "Project Phase")]
        public string Name { get; set; }

        [Display(Name = "Approved ?")]
        public bool  Approved { get; set; }
    }
}

  