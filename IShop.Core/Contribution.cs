using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Contribution
    {
        public int Id { get; set; }
        public int Project_No { get; set; }
        public DateTime CreatedOn { get; set; }

        public int ChildId { get; set; }
        public Decimal Paid { get; set; }

        [Display(Name = "Payment for Month?")]
        public string Month { get; set; }
        public Decimal Total { get; set; }
        public Decimal Balance { get; set; }
    }
}
