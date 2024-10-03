using Ishop.Infa;
using Ishop.Models;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           
            Immigrant_context II = new Immigrant_context();
            var Imm = II.immigrants.Count();
            ViewBag.Imm = Imm;


            Admission_context AA = new Admission_context();
            var Admin = AA.admissions.Count();
            ViewBag.Admin = Admin;

            return View();
        }





        
    }
}