using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Direct
    {
        public int Id { get; set; }

        public int Step { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string User { get; set; }


        [Display(Name = "Theme Color")]
        public string Theme_Color { get; set; }
    }
}
