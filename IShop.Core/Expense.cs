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

        
        [Display(Name = "Fuliza Charges ")]
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public Decimal Fuliza { get; set; }


        public Decimal Total { get; set; }

        [Display(Name = "Transaction cost")]
        public Decimal Transaction_cost { get; set; }


        [Required]
        public string Item { get; set; }

        [Required]
        [Display(Name = "Payment Mode")]
        public string Mode { get; set; }


        [Required]
        [Display(Name = "Notes")]
        public string Additional_Notes { get; set; }

    }
}