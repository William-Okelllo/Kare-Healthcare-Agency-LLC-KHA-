using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Collection
    {
        public int Id { get; set; }
        [Display(Name = "Phase Id ")]
        public int Phase_Id { get; set; }
        [Display(Name = "Project Id ")]
        public int Project_Id { get; set; }

        [Display(Name = "Date ")]
        public DateTime CreatedOn { get; set; }
        public decimal Charge { get; set; }
    }
}
