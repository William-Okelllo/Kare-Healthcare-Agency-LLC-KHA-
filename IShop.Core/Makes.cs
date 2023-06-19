using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IShop.Core
{
    public class Makes
    {
        public int Id { get; set; }
        [Display(Name = "Make")]
        public string Make { get; set; }
        [Display(Name = "Code")]
        public int Code { get; set; }
    }
}
