using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Activities
    {
        public int Id { get; set; }
        public int Hours { get; set; }
        public int TimesheetId { get; set; }
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Project Name")]
        public string Project_Name { get; set; }
        public string Day { get; set; }
        public string User { get; set; }
        [Display(Name = "Project Phase")]
        public string Name { get; set; }
        public string Comments { get; set; }
        
    }
}
