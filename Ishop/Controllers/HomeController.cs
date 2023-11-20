using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using IShop.Core.Interface;
using Microsoft.AspNet.SignalR.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;

namespace Ishop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {


            Timesheet_Context context = new Timesheet_Context();

            // Get the integer value of the current day of the week (0 for Sunday, 1 for Monday, etc.)
            int currentDayOfWeek = (int)DateTime.Now.DayOfWeek;

            var Time = context.timesheets.Where(c => c.CreatedOn.Day == currentDayOfWeek);
            bool dataExists = Time.Any();

            if (dataExists)
            {
                ViewBag.Timesheet = "Todays' timesheet already submitted successfully";
            }
            else
            {
                if (currentDayOfWeek == 0)
                {
                    ViewBag.Timesheet = "Today's Sunday not mandatory to submit timesheet";
                }
                else
                {
                    ViewBag.Timesheet = "Todays' timesheet still pending for submission";
                }
            }





            return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}