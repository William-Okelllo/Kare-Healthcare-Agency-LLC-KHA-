using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Days_leave
    {
        public int Id { get; set; }


        public int Leave_Id { get; set; }

        public bool Approved { get; set; }

        [Display(Name = "Leave Type")]
        [Required]
        public string Type { get; set; }

        [Display(Name = "From Date")]
        public DateTime From_Date { get; set; }

        [Display(Name = "To Date")]
        public DateTime To_Date { get; set; }

        public Decimal Days { get; set; }

        public int Time { get; set; }
    }
}
