using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Expense
    {
        public int Id { get; set; }


        [Display(Name = " Generated On")]
        public DateTime CreatedOn { get; set; }


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