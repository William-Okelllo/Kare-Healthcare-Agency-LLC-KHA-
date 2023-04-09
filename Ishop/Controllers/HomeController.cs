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


            sp_allprocs l9 = new sp_allprocs();
            var boo9 = l9.sp_dash(User.Identity.Name).ToList();
            ViewBag.l9 = boo9;




            return View();
        }

        public ActionResult Dash()
        {
            sp_allprocs l9 = new sp_allprocs();
            var boo9 = l9.sp_dash(User.Identity.Name).ToList();
            ViewBag.l9 = boo9;
            

            Cashmate_sp_das A2 = new Cashmate_sp_das();
            var Dash2 = A2.sp_dash_2(User.Identity.Name).ToList();
            ViewBag.A2 = Dash2;

            Cashmate_sp_das A3 = new Cashmate_sp_das();
            var Dash3 = A3.sp_dash_3(User.Identity.Name).ToList();
            ViewBag.A3 = Dash3;

            Cashmate_sp_das A4 = new Cashmate_sp_das();
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