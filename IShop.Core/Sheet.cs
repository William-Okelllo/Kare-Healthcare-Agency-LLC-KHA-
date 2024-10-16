using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Sheet
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee Name ")]
        public String Employee { get; set; }

        public DateTime Added { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start TIme ")]
        public TimeSpan Start_time { get; set; }
        [DataType(DataType.Time)]

        [Display(Name = "End Time  ")]
        public TimeSpan End_time { get; set; }

        public String Position { get; set; }

        public String Staff { get; set; }

        public Decimal Hours { get; set; }

        [Display(Name = "Brief Work Description  ")]
        public String Description { get; set; }
    }
}
