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
            sp_dash l1 = new sp_dash();
            var boo1 = l1.Dash_1().ToList();
            ViewBag.l1 = boo1;

            sp_dash l2 = new sp_dash();
            var boo2 = l2.Dash_2().ToList();
            ViewBag.l2 = boo2;

            sp_dash l3 = new sp_dash();
            var boo3 = l3.Dash_3().ToList();
            ViewBag.l3 = boo3;

           

            return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}