//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ishop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ticket_logs
    {
        public int id { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string Action { get; set; }
        public Nullable<int> Ticket_id { get; set; }
        public string User_Acc { get; set; }
    }
}
