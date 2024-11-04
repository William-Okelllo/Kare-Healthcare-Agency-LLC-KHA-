using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Admission
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Contacts { get; set; }

        public string Email { get; set; }


        public string Gender { get; set; }

        [Display(Name = "Date of Admission")]
        public DateTime Date { get; set; }


        [Display(Name = "Admission No")]
        public string Admin_No { get; set; }


        [Display(Name = "Address (street address, city, zip code)")]
        public string Address { get; set; }


        [Display(Name = "Highest Education level")]
        public string Level_Education { get; set; }

        [Display(Name = "Employment Status ")]
        public string Employement_Status { get; set; }



        [Display(Name = "Name ")]
        public string Em_Name { get; set; }

        [Display(Name = "Phone ")]
        public string Em_Phone { get; set; }

        [Display(Name = "Mail ")]
        public string Em_Mail { get; set; }

        [Display(Name = "Can We text? ")]
        public bool Text { get; set; }


        [Display(Name = "Comments about Student ")]
        public string Comments { get; set; }


        public string Ethinicity { get; set; }
        public string Language { get; set; }
   






    }
}
