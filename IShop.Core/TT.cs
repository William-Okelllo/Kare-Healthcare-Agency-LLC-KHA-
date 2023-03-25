using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class TT
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Account Name ")]
        public String Employee { get; set; }

        
        [Display(Name =  "Posted On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Display(Name = "Duties Done ")]
        public String Description { get; set; }

        [Display(Name = "Project ")]
        public String Project { get; set; }

        [Display(Name = "feedback ")]
        public String feedback { get; set; }

    }
}
