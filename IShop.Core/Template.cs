using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Template
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        [Display(Name = "Template?")]
        public string Code { get; set; }

        public string Content { get; set; }

    }
}
