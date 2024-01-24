using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class leaves_Types
    {
        public int Id { get; set; }

        [Display(Name = "Order No")]
        public int Steps { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
        public string Type { get; set; }
        public Decimal Days { get; set; }
    }
}
