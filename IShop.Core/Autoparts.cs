using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Autoparts
    {
        public int Id { get; set; }

        
        [Display(Name = "Part Name ")]
        public String Part_Name { get; set; }

        [Display(Name = "Part Category ")]
        public String Category { get; set; }

        [Display(Name = "Added on")]
        public DateTime CreatedOn { get; set; }
    }
}
