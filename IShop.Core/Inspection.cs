using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Inspection
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        [Display(Name = "Booking Id ")]
        public int Booking_Id { get; set; }
        public String Vehicle { get; set; }
        public String Vehicle_Reg { get; set; }
        public String Phone { get; set; }
        public String Description { get; set; }
        public String Customer { get; set; }
        public String staff { get; set; }
        [Display(Name = "Estimate status ")]
        public int Inspection_Status { get; set; }
        public decimal Estimate { get; set; }
        [Display(Name = "Inspection Total ")]
        public decimal Inspection_Total { get; set; }
    }
}
