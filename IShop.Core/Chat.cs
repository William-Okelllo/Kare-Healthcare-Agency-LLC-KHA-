using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Chat
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "From ")]
        public String From { get; set; }

        [Required]
        [Display(Name = "To ")]
        public String To { get; set; }


        [Display(Name = "Registered On ")]
        public DateTime CreatedOn { get; set; }


        [Required]
        [Display(Name = "To ")]
        public String message { get; set; }



    }
}
