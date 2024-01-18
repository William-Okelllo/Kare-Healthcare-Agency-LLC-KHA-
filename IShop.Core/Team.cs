using System;
using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Team
    {
        public int Id { get; set; }

        [Display(Name = "Allocated Hours ")]
        public decimal Hours { get; set; }
        public int Project_id { get; set; }

        [Display(Name = "Project Name ")]
        public string Project_Name { get; set; }
        [Display(Name = "Added on ")]
        public DateTime CreatedOn { get; set; }
        public string Username { get; set; }

        [Display(Name = "Phase")]
        public string Name { get; set; }
    }
}
