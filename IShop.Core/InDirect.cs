using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Indirect
    {
        public int Id { get; set; }


        [Display(Name = "Plan No")]
        public int WorkPlan_Id { get; set; }

        public string Description { get; set; }


       
        public decimal Rate { get; set; }

        public decimal Amount { get; set; }
    }
}