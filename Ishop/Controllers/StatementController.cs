using Ishop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class StatementController : Controller
    {
        // GET: Statement
        public ActionResult Index( )
        {


            sp_statement_v2 l9 = new sp_statement_v2();
            var boo9 = l9.sp_monthly_statement(User.Identity.Name).ToList();
            ViewBag.l9 = boo9;

            sp_statement_v2 l8 = new sp_statement_v2();
            var boo8 = l8.sp_statement_footer(User.Identity.Name).ToList();
            ViewBag.l8 = boo8;


            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;
            return View();
        }
    }
}