using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Benitem
    {
        public int Id { get; set; }


        [Display(Name = "Tag")]
        public int Beneficiary_Id { get; set; }

        [Display(Name = "Beneficiary name")]
        public string Name { get; set; }

        public int Quantity { get; set; }


        [Display(Name = "Rate")]
        public Decimal Each { get; set; }
        public Decimal Total { get; set; }
    }
}
