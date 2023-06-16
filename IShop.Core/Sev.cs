using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Sev
    {
        public int Id { get; set; }



        [Display(Name = "Service Name")]
        public string Service_Name { get; set; }

        [Display(Name = "Added on")]
        public DateTime CreatedOn { get; set; }

        public bool Taxable { get; set; }
    }
}
