using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Job
    {
        public int Id { get; set; }

   
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Application Deadline")]
        public DateTime Application_Deadline { get; set; }

        public string Sector { get; set; }

        public string Experience { get; set; }
       
        public string Qualifications { get; set; }

        public string Type { get; set; }
        [Display(Name = "Application Type")]
        public string Application_Type { get; set; }


        public Decimal Salary { get; set; }
       
    }
}
