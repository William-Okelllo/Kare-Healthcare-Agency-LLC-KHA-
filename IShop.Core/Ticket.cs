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

        [Required]
        public String CONS { get; set; }

        [Required]
        public String Airline { get; set; }

        [Required]
        [Display(Name = "Service Provider")]
        public String Service_Provider { get; set; }

        [Required]
        [Display(Name = "Pax Name")]
        public String Pax_Name { get; set; }

        
        public String Client { get; set; }
        public String Currency { get; set; }
        public decimal Inv_Amount { get; set; }

        public decimal Net_Amount { get; set; }

        public decimal Gross_Profit { get; set; }

        public decimal Incentv { get; set; }

        public decimal Recovery { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        public DateTime Departure_Date { get; set; }

        [Required]
        [Display(Name = "Arrival_Date")]
        public DateTime Arrival_Date { get; set; }

        [Required]
        public string Routing { get; set; }

        [Required]
        public string CTC { get; set; }

       


        [Required]
        public string Remarks { get; set; }


        public int Ticket_status { get; set; }

        public string Staff { get; set; }

        [Required]
        [Display(Name = "Payment Mode")]
        public string Mode { get; set; }
    }

}
