using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IShop.Core
{
    public class Bug
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string Support_Message { get; set; }

        public string Posted_By { get; set; }

        public string severity { get; set; }

        public string Status { get; set; }

        public DateTime Post_Date { get; set; }
    }
}
