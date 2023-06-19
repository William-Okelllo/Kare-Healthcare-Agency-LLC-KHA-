using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Invoice
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        [Display(Name = "Booking Id ")]
        public int Booking_Id { get; set; }
        public String Vehicle { get; set; }
        public String Vehicle_Reg { get; set; }
        public String Phone { get; set; }
        public String Customer { get; set; }
        public String staff { get; set; }

        [Display(Name = "Invoice Status ")]

        public int Invoice_Status { get; set; }

        public decimal VAT { get; set; }

        [Display(Name = "Invoice_Amount ")]
        public decimal Invoice_Amount { get; set; }

    }
}