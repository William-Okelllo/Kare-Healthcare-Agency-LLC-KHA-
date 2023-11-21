using System;

namespace IShop.Core
{
    public class OutgoingEmails
    {
        public int Id { get; set; }


        public int? Trials { get; set; }
        public string Response { get; set; }

        public DateTime CreatedOn { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }

        public string Body { get; set; }
        public bool Status { get; set; }

        public string Files { get; set; }
    }
}
