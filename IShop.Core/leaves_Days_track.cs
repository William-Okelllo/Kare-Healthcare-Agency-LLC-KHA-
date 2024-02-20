using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class leaves_Days_track
    {
        public int Id { get; set; }
        [Display(Name = "Intial Total Days")]
        public decimal Total_leaves_per_year { get; set; }

        [Display(Name = "Requested  Days")]
        public decimal Requested_leaves { get; set; }


        [Display(Name = "Leave Type ")]
        public string Type { get; set; }

        [Display(Name = "Balance")]
        public decimal Remaining_leaves { get; set; }
        public string Username { get; set; }
    }
}
