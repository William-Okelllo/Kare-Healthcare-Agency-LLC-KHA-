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
            OutgoingEmailsContext NN = new OutgoingEmailsContext();
           Userstable EE = new Userstable();
           var Empmail = EE.AspNetUsers.Where(c => c.UserName == User.Identity.Name).FirstOrDefault();
            var noti = NN.outgoingEmails.OrderByDescending(c => c.Id).Where(c => c.Recipient == Empmail.Email).ToList();
            ViewBag.Noti = noti;

            HodContext DD = new HodContext();
            var Depart = DD.hODs.Where(d => d.Staff == User.Identity.Name).Select(d => d.DprtName).ToList();
            ViewBag.Departmentt = Depart;

            ViewBag.Receiver = Empmail.Email;
           


            Timesheet_Context TT = new Timesheet_Context();
            var Sheet = TT.timesheets.Where(i => i.Owner == User.Identity.Name && DateTime.Now >= i.From_Date && DateTime.Now <= i.End_Date).FirstOrDefault();
            if(Sheet != null)
            {
                ViewBag.DirectHours = Sheet.Direct_Hours;
                ViewBag.InDirectHours = Sheet.InDirect_Hours;
                ViewBag.leave = Sheet.Leave;
                ViewBag.Total = Sheet.Tt;
            }
            else
            {
                ViewBag.DirectHours = 0;
                ViewBag.InDirectHours = 0;
                ViewBag.leave = 0;
                ViewBag.Total = 0;
            }


            return View();
        }





        
    }
}