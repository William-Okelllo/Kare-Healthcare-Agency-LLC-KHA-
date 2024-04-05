using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Holiday
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Holiday Name")]
        public  string Holiday_Name { get; set; }

        [Display(Name = "Holiday Date")]
        public DateTime Holiday_date { get; set; }


        [Display(Name = "Assigned Hours")]
        public  Decimal Hours_Assigned { get; set; }

        
        public bool Activated { get; set; }

    }
}
