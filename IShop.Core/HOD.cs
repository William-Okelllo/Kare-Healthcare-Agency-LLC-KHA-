using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public  class HOD
    {
        public int Id { get; set; }

        public int Department_Id { get; set; }

        [Required]
        [Display(Name = "Department Name ")]
        public String DprtName { get; set; }

        public String Staff { get; set; }

    }
}
