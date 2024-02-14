using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class ReportAccess
    {
        public int Id { get; set; }
        public DateTime GrantedOn { get; set; }

        public string Report { get; set; }
        public int GroupId { get; set; }

        public string Staff { get; set; }
    }
}
