using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ishop.Models
{
    public class ApiResponse
    {
        public int response_code { get; set; }
        public string Credit { get; set; }
        public string PartnerId { get; set; }
    }
}