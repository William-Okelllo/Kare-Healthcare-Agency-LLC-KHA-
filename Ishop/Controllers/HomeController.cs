using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EventsContext EEE =new EventsContext();
            var Data = EEE.events.Where(c=>c.staff ==User.Identity.Name);
            var EventId =EEE.events.Where(c=>c.staff == User.Identity.Name).FirstOrDefault();
            ViewBag.MyEvents = Data;
            

           

            return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}