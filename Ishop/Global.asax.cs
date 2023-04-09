using Ishop.Infa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ishop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<InvoiceContext>(null);
            Database.SetInitializer<leaveContext>(null);
            Database.SetInitializer<DataContext>(null);
            Database.SetInitializer<AccessContext>(null);
            Database.SetInitializer<StaffContext>(null);
            Database.SetInitializer<Employee_Context>(null);
            Database.SetInitializer<DepartmentContext>(null);
            Database.SetInitializer<TicketContext>(null);
            Database.SetInitializer<AttendanceContext>(null);
            Database.SetInitializer<ExpenseContext>(null);
            Database.SetInitializer<Recovery_context>(null);
            Database.SetInitializer<Advance_Context>(null);

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
