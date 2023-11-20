using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class leaves_Days_track
    {
        public int Id { get; set; }
        public int Total_leaves_per_year { get; set; }
        public int Requested_leaves { get; set; }
        public string Type { get; set; }
        public int Remaining_leaves { get; set; }
        public string Username { get; set; }
    }
}
