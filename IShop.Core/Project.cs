using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Project
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        [Display(Name = "Project Name")]
        public string Project_Name { get; set; }


        [Display(Name = "Current Phase")]
        public string Name { get; set; }
        public string location { get; set; }
        public string User { get; set; }
        public bool Status { get; set; }
    }
}
