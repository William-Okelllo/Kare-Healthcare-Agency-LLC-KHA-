﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Timesheet
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [Display(Name = "Staff Name")]
        public string Owner { get; set; }
       

        
    }
}
