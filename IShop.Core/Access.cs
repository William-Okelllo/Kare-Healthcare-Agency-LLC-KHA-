using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Access
    {
        public int id { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string Role { get; set; }

        public string Status { get; set; }
    }

}