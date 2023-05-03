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
            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;


            sp_Charles_M l9 = new sp_Charles_M();
            var boo9 = l9.sp_dash(User.Identity.Name).ToList();
            ViewBag.l9 = boo9;



            sp_Charles_M l8 = new sp_Charles_M();
            var boo8 = l8.sp_dash_Income(User.Identity.Name).ToList();
            ViewBag.l8 = boo8;


            return View();
        }

        public ActionResult Dash()
        {
            sp_Charles_M l9 = new sp_Charles_M();
            var boo9 = l9.sp_dash(User.Identity.Name).ToList();
            ViewBag.l9 = boo9;


            sp_Charles_M A2 = new sp_Charles_M();
            var Dash2 = A2.sp_dash_2(User.Identity.Name).ToList();
            ViewBag.A2 = Dash2;

            sp_Charles_M A3 = new sp_Charles_M();
            var Dash3 = A3.sp_dash_3(User.Identity.Name).ToList();
            ViewBag.A3 = Dash3;

            sp_Charles_M A4 = new sp_Charles_M();
            var Dash4 = A4.sp_dash_4(User.Identity.Name).ToList();
            ViewBag.A4 = Dash4;

            

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}