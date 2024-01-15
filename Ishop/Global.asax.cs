using Ishop.Infa;
using System.Data.Entity;
using System.Net;
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<InDirect_Context>(null);
            Database.SetInitializer<Direct_Context>(null);
            Database.SetInitializer<DataContext>(null);
            Database.SetInitializer<AccessContext>(null);
            Database.SetInitializer<Timesheet_Context>(null);
            Database.SetInitializer<Activities_Context>(null);
            Database.SetInitializer<Employee_Context>(null);
            Database.SetInitializer<Project_Context>(null);
            Database.SetInitializer<Phase_Context>(null);
            Database.SetInitializer<Team_Context>(null);
            Database.SetInitializer<ConfigsContext>(null);
            Database.SetInitializer<IDays_leave_context>(null);
            Database.SetInitializer<Direct_Activities_Context>(null);
            Database.SetInitializer<Indirect_Activities_Context>(null);
            Database.SetInitializer<leaveTypesContext>(null);
            Database.SetInitializer<DepartmentContext>(null);
        }
    }
}
