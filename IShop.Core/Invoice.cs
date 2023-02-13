using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Invoice
    {
        public int Id { get; set; }

        

        [Required]
        [Display(Name = "Client Name")]
        public string Client_Name { get; set; }

        [Display(Name = " Generated On")]
        public DateTime CreatedOn { get; set; }



        [Display(Name = " Due Date")]
        public DateTime Due_Date { get; set; }

        [Required]
        public Decimal Amount { get; set; }


        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }


        [Required]
        [Display(Name = "Notes")]
        public string Additional_Notes { get; set; }


    }
}
