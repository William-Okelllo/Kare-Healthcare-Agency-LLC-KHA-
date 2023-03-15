using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Ticket
    {
        public int id { get; set; }

        [Display(Name = "Posted On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Issued On")]
        public DateTime issue_Date { get; set; }

        [Display(Name = "Ticket/Ref NO")]
        public String Ticket_No { get; set; }

        [Display(Name = "Is Grp Ticket?")]
        public bool Group_ticket { get; set; }

        [Display(Name = "Invoiced As One?")]
        public bool Invoice_As_One { get; set; }


        public String Group_id { get; set; }

        public String Airline { get; set; }

       
        [Display(Name = "Service Provider")]
        public String Service_Provider { get; set; }

        [Required]
        [Display(Name = "Pax Name")]
        public String Pax_Name { get; set; }

        
        public String Client { get; set; }
        public String Currency { get; set; }

        [Required]
        public decimal Inv_Amount { get; set; }
        [Required]
        public decimal Net_Amount { get; set; }

        public decimal Gross_Profit { get; set; }

        public decimal Incentv { get; set; }

        public decimal Recovery { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public DateTime Departure_Date { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        public DateTime Arrival_Date { get; set; }

       
        public string Routing { get; set; }

        public string Payer { get; set; }
        [Display(Name = "Ticketing System")]
        public string Ticketing_System { get; set; }

        [Required]
        public string CTC { get; set; }

       


        [Required]
        public string Remarks { get; set; }


        public int Ticket_status { get; set; }

        public string Staff { get; set; }

      
        [Display(Name = "Payment Mode")]
        public string Mode { get; set; }
    }

}
