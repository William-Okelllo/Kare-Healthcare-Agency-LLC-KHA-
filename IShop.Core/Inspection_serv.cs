using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Inspection_serv
    {
        public int Id { get; set; }
        [Display(Name = "Card Id")]
        public int Booking_Id { get; set; }
        [Display(Name = "Service Name ")]
        public String Service_Name { get; set; }
        public decimal Estimate_Totals { get; set; }
        public decimal Amount { get; set; }
        public decimal Total_Amount { get; set; }
        public bool VAT { get; set; }
    }
}
