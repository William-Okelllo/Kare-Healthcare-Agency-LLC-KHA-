using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Recovery
    {
        public int id { get; set; }

        [Display(Name = "Ticket id")]
        public int Ticket_id { get; set; }

        [Display(Name = "Ticket/Ref NO")]
        public String Ticket_No { get; set; }

        [Display(Name = "Posted On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        public String Staff { get; set; }

        [Display(Name = "Invoice Number")]
        public string Inv_No { get; set; }
        [Display(Name = "Invoice Amount")]
        public decimal Inv_Amount { get; set; }

        [Display(Name = "Amount Recovered")]
        public decimal Amount_Recovered { get; set; }
        public decimal Balance { get; set; }
    }
}
