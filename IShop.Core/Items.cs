using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Items
    {
        public int Id { get; set; }

        [Display(Name = "beneficiary Item")]
        public string Name { get; set; }


        public decimal Amount { get; set; }
    }
}
