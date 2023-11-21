using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }
        public partial class webConfigFile : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
                //Helps to open the Root level web.config file.
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Email"].Value = "Email";
                webConfigApp.AppSettings.Settings["Password"].Value = "Password";
                webConfigApp.AppSettings.Settings["FileSize"].Value = "FileSize";
                webConfigApp.AppSettings.Settings["FileType"].Value = "FileType";

                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
            }
        }


    }
}
