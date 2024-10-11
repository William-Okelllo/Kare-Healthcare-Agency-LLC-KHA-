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
            Database.SetInitializer<DataContext>(null);
            Database.SetInitializer<AccessContext>(null);
            Database.SetInitializer<Activities_Context>(null);
            Database.SetInitializer<Workplan_context>(null);
            Database.SetInitializer<ConfigsContext>(null);
            Database.SetInitializer<DepartmentContext>(null);
            Database.SetInitializer<Perform_context>(null);
            Database.SetInitializer<Item_context>(null);
            Database.SetInitializer<Benef_context>(null);
            Database.SetInitializer<benitem_context>(null);
            Database.SetInitializer<Grant_context>(null);
            Database.SetInitializer<Exp_context>(null);
            Database.SetInitializer<Rpt_context>(null);
            Database.SetInitializer<Admission_context>(null);



        }
    }
}
