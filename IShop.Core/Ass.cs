using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Ass
    {
        public int Id { get; set; }

        [Display(Name = "Beneficiary name")]
        public string Name { get; set; }

    }
}
