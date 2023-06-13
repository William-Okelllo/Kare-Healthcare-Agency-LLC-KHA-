﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Part
    {
        public int Id { get; set; }
        [Display(Name = "Card Id")]
        public int Booking_Id { get; set; }
        [Display(Name = "part Name ")]
        public String Part_Name { get; set; }

        public String Condition { get; set; }
        public decimal Amount { get; set; }

    }
}
