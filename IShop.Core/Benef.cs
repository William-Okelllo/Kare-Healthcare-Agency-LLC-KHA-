using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Benef
    {
        public int Id { get; set; }
        
        [Display(Name = "Beneficiary name")]
        public string Name { get; set; }

        public string Contacts { get; set; }

        public string Country { get; set; }
        public string Email { get; set; }


        public Decimal Total { get; set; }
        

    }
}
