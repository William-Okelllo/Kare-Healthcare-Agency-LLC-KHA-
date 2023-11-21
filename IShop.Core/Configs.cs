using System.ComponentModel.DataAnnotations;

namespace IShop.Core
{
    public class Configs
    {
        public int Id { get; set; }

        public string Smtp { get; set; }
        public string Email { get; set; }

        [Display(Name = "Timesheet Setup Day")]
        public string RunTime { get; set; }
        public string Password { get; set; }
        public string SSRSReportsUrl { get; set; }

        [Display(Name = "System link")]
        public string Business_mail { get; set; }
        public string port { get; set; }
    }
}
