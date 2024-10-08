﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Immigrant
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Contacts { get; set; }

        public string Email { get; set; }

        [Display(Name = "Arrival Year")]
        public int Year { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }

        [Display(Name = "From Which Country")]
        public string Country { get; set; }


        [Display(Name = "Social security?")]
        public bool Social_security { get; set; }


        [Display(Name = "Work Permit?")]
        public bool Work_Permit { get; set; }

        

    }
}
