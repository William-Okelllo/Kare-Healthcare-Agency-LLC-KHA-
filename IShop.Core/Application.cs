using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Application
    {
        public int Id { get; set; }
        public int Job_id { get; set; }
        public string Job_Title { get; set; }     
        public DateTime Application_Date { get; set; }
        public string Applicant { get; set; }
        public DateTime Application_Deadline { get; set; }
        public string Posted_By { get; set; }
        public string Sector { get; set; }

        public string Experience { get; set; }
        [Display(Name = "External link ")]
        public string link_email { get; set; }
        public string Qualifications { get; set; }

        public string Type { get; set; }
        [Display(Name = "Application Type")]
        public string Application_Type { get; set; }
        [Display(Name = "Show Salary")]
        public bool Show_salary { get; set; }

        public Decimal Salary { get; set; }


    }
}
