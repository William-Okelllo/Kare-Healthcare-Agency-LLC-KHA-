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
    
    public partial class sp_monthly_statement_Result
    {
        public int id { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Fuliza { get; set; }
        public Nullable<decimal> Transaction_cost { get; set; }
        public string Item { get; set; }
        public string Additional_Notes { get; set; }
        public Nullable<decimal> Total { get; set; }
        public string Mode { get; set; }
    }
}