using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Template
    {
       public int Id {get; set;}

        public DateTime CreatedOn { get; set;}
        [Display(Name = "Template?")]
        public string Code { get; set;}

        public string Content { get; set;}
       
    }
}
