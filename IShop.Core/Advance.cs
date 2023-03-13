using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Advance
    {
        public int id { get; set; }

        [Display(Name = "Staff")]
        public string Employee { get; set; }

        [Display(Name = "Posted On")]
        public DateTime CreatedOn { get; set; }

        public string Employee_Fullnames { get; set; }

        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public string Amount_Words { get; set; }

        [Display(Name = "Pay Date")]
        public DateTime Pay_Date { get; set; }

        [Display(Name = "Due to")]
        public DateTime Due_to { get; set; }

        [Display(Name = "Manager Approvals")]
        public string Approved_by_Manager { get; set; }

     

        [Display(Name = "Finance Approvals")]
        public string Approved_by_Finance { get; set; }

     


    }
}
