using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Direct_cost
    {
        public int Id { get; set; }


        [Display(Name = "Plan No")]
        public int WorkPlan_Id { get; set; }

        public string Description { get; set; }


        public int Quantity { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal Unit { get; set; }

        public decimal Total { get; set; }
    }
}
