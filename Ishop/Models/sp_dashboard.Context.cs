﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class sp_dashboards : DbContext
    {
        public sp_dashboards()
            : base("name=sp_dashboards")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<Dashboard_1_Result> Dashboard_1(string staff, string airline, string service_Provider, string startDate, string endDate)
        {
            var staffParameter = staff != null ?
                new ObjectParameter("Staff", staff) :
                new ObjectParameter("Staff", typeof(string));
    
            var airlineParameter = airline != null ?
                new ObjectParameter("Airline", airline) :
                new ObjectParameter("Airline", typeof(string));
    
            var service_ProviderParameter = service_Provider != null ?
                new ObjectParameter("Service_Provider", service_Provider) :
                new ObjectParameter("Service_Provider", typeof(string));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Dashboard_1_Result>("Dashboard_1", staffParameter, airlineParameter, service_ProviderParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<Dashboard_2_Result> Dashboard_2(string staff, string airline, string service_Provider, string startDate, string endDate)
        {
            var staffParameter = staff != null ?
                new ObjectParameter("Staff", staff) :
                new ObjectParameter("Staff", typeof(string));
    
            var airlineParameter = airline != null ?
                new ObjectParameter("Airline", airline) :
                new ObjectParameter("Airline", typeof(string));
    
            var service_ProviderParameter = service_Provider != null ?
                new ObjectParameter("Service_Provider", service_Provider) :
                new ObjectParameter("Service_Provider", typeof(string));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Dashboard_2_Result>("Dashboard_2", staffParameter, airlineParameter, service_ProviderParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<Dashboard_3_Result> Dashboard_3(string staff, string airline, string service_Provider, string startDate, string endDate)
        {
            var staffParameter = staff != null ?
                new ObjectParameter("Staff", staff) :
                new ObjectParameter("Staff", typeof(string));
    
            var airlineParameter = airline != null ?
                new ObjectParameter("Airline", airline) :
                new ObjectParameter("Airline", typeof(string));
    
            var service_ProviderParameter = service_Provider != null ?
                new ObjectParameter("Service_Provider", service_Provider) :
                new ObjectParameter("Service_Provider", typeof(string));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Dashboard_3_Result>("Dashboard_3", staffParameter, airlineParameter, service_ProviderParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<Dashboard_4_Result> Dashboard_4(string staff, string airline, string service_Provider, string startDate, string endDate)
        {
            var staffParameter = staff != null ?
                new ObjectParameter("Staff", staff) :
                new ObjectParameter("Staff", typeof(string));
    
            var airlineParameter = airline != null ?
                new ObjectParameter("Airline", airline) :
                new ObjectParameter("Airline", typeof(string));
    
            var service_ProviderParameter = service_Provider != null ?
                new ObjectParameter("Service_Provider", service_Provider) :
                new ObjectParameter("Service_Provider", typeof(string));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Dashboard_4_Result>("Dashboard_4", staffParameter, airlineParameter, service_ProviderParameter, startDateParameter, endDateParameter);
        }
    }
}
