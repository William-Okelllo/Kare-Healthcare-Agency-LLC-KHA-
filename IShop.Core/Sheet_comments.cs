using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Sheet_comments
    {
        public int Id { get; set; }


        [Display(Name = "Timesheet Id ")]
        public int Timesheeet_Id { get; set; }

        [Display(Name = " Date ")]
        public DateTime CreatedOn { get; set; }
        public string Approver { get; set; }
        public string Comments { get; set; }
    }
}
