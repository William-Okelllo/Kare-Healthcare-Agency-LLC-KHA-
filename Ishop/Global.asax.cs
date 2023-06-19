using Ishop.Infa;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<CheckInContext>(null);
            Database.SetInitializer<Cardcontext>(null);
            Database.SetInitializer<DataContext>(null);
            Database.SetInitializer<AccessContext>(null);
            Database.SetInitializer<AutoContext>(null);
            Database.SetInitializer<Inspection_Context>(null);
            Database.SetInitializer<Inspections_partsContext>(null);
            Database.SetInitializer<Inspec_serv_context>(null);
            Database.SetInitializer<Invoice_Context>(null);
            Database.SetInitializer<Makes_context>(null);

        }
    }
}
