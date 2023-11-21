using System;

namespace IShop.Core
{
    public class Outgoingsms
    {
        public int Id { get; set; }
        public string RecipientNumber { get; set; }
        public int? Trials { get; set; }
        public string Response { get; set; }
        public string Code { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MessageText { get; set; }
        public bool IsSent { get; set; }
    }
}
