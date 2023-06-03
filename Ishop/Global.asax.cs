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
          
            Database.SetInitializer<DataContext>(null);
            Database.SetInitializer<AccessContext>(null);
            Database.SetInitializer<StaffContext>(null);
            Database.SetInitializer<Employee_Context>(null);
            Database.SetInitializer<Job_context>(null);
            Database.SetInitializer<ProfileContext>(null);
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
