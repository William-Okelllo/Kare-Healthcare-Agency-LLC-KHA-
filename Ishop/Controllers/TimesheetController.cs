using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using IShop.Core.Interface;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Deployment.Internal;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Ishop
{
    
    public class TimesheetController : Controller
    {
        private Userstable dbb = new Userstable();
        private Timesheet_Context db = new Timesheet_Context();

        [Authorize]
        public ActionResult Index()
        {


            DateTime currentDate = DateTime.Now;

            // Set Monday to the Monday of the current week
            DateTime Monday = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

            // Set Sunday to the next Sunday from the current day
            DateTime Sunday = Monday.AddDays(6);

            ViewBag.Monday = Monday;
            ViewBag.Sunday = Sunday;
            var Sheet = db.timesheets.Where(i => i.Owner == User.Identity.Name &&  i.From_Date ==Monday && i.End_Date==Sunday).FirstOrDefault();
            ViewBag.Sheet= Sheet;



            List<A> results = new List<A>();
            string strcon = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                sqlCon.Open();

                using (SqlCommand command = new SqlCommand("sp_Calendar_Activities", sqlCon))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", User.Identity.Name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            A result = new A
                            {
                                WeekdayName = reader["WeekdayName"].ToString(),
                                DateValue = ((DateTime)reader["DateValue"]).Date,
                                Direct_Hours = Convert.ToInt32(reader["Direct Hours"]),
                                InDirect_Hours = Convert.ToInt32(reader["InDirect Hours"]),
                                Total_Hours = Convert.ToInt32(reader["Total Hours"]),
                                WeekNumber = Convert.ToInt32(reader["WeekNumber"]),
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            ViewBag.Results = results;











            return View();
        }

        public ActionResult Details(int? id)
        {
            var Times = db.timesheets.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.timesheet = Times;

            Employee_Context EC = new Employee_Context();
            var employee = EC.employees.Where(c => c.Username == Times.Owner).FirstOrDefault();

            ViewBag.Employee = employee;


            Timesheet timesheet = db.timesheets.Find(id);

            DateTime startDate = timesheet.From_Date;
            DateTime endDate = timesheet.End_Date;

            // Generate dates between start and end dates
            List<DateTime> dateRange = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            // Group dates by weekday
            var groupedDates = dateRange.GroupBy(date => date.DayOfWeek)
                .ToDictionary(group => group.Key, group => group.ToList());

            // Pass the grouped dates to the view using ViewBag
            ViewBag.GroupedDates = groupedDates;




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }





       










            public ActionResult Approval(int? id)
        {


            Timesheet timesheet = db.timesheets.Find(id);

            DateTime startDate = timesheet.From_Date;
            DateTime endDate = timesheet.End_Date;

            // Generate dates between start and end dates
            List<DateTime> dateRange = Enumerable.Range(0, 1 + endDate.Subtract(startDate).Days)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            // Group dates by weekday
            var groupedDates = dateRange.GroupBy(date => date.DayOfWeek)
                .ToDictionary(group => group.Key, group => group.ToList());

            // Pass the grouped dates to the view using ViewBag
            ViewBag.GroupedDates = groupedDates;




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        [Authorize]
        public ActionResult Mine (string searchBy, string search, int? page, string Duration)
        {

            DateTime currentDate = DateTime.Now;

            // Set Monday to the Monday of the current week
            DateTime Monday = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

            // Set Sunday to the next Sunday from the current day
            DateTime Sunday = Monday.AddDays(6);

            // Determine date range based on Duration parameter
            DateTime startRange, endRange;

            switch (Duration)
            {
                case "thisWeek":
                    startRange = Monday;
                    endRange = Sunday;
                    break;

                case "lastWeek":
                    startRange = Monday.AddDays(-7);
                    endRange = Sunday.AddDays(-7);
                    break;

                case "thisMonth":
                    startRange = new DateTime(currentDate.Year, currentDate.Month, 1);
                    endRange = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                    break;

                case "lastMonth":
                    startRange = new DateTime(currentDate.Year, currentDate.Month - 1, 1);
                    endRange = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);
                    break;

                default:
                    // Use the default date range for other cases
                    startRange = Monday;
                    endRange = Sunday;
                    break;
            }

            ViewBag.StartDate = startRange;
            ViewBag.EndDate = endRange;

            return View(db.timesheets.OrderByDescending(p => p.CreatedOn).Where(c => c.Owner == User.Identity.Name &&
            (Duration == null || Duration == "" || (c.From_Date >= startRange && c.From_Date <= endRange))).ToList().ToPagedList(page ?? 1, 11));

                
           
        }




        [Authorize]

        public ActionResult All(int? page, string option, string startDate, string endDate, string Duration)
        {
            DateTime currentDate = DateTime.Now;

            // Set Monday to the Monday of the current week
            DateTime Monday = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

            // Set Sunday to the next Sunday from the current day
            DateTime Sunday = Monday.AddDays(6);

            // Determine date range based on Duration parameter
            DateTime startRange, endRange;

            switch (Duration)
            {
                case "thisWeek":
                    startRange = Monday;
                    endRange = Sunday;
                    break;

                case "lastWeek":
                    startRange = Monday.AddDays(-7);
                    endRange = Sunday.AddDays(-7);
                    break;

                case "thisMonth":
                    startRange = new DateTime(currentDate.Year, currentDate.Month, 1);
                    endRange = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                    break;

                case "lastMonth":
                    startRange = new DateTime(currentDate.Year, currentDate.Month - 1, 1);
                    endRange = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);
                    break;

                default:
                    // Use the default date range for other cases
                    startRange = Monday;
                    endRange = Sunday;
                    break;
            }

            ViewBag.StartDate = startRange;
            ViewBag.EndDate = endRange;

            HodContext DD = new HodContext();
            var Depart = DD.hODs.Where(D => D.Staff == User.Identity.Name).Select(d => d.DprtName).ToList();

            var Employ = db.timesheets.Where(c => Depart.Contains(c.Department)).Select(t => t.Owner).Distinct().ToList();
            ViewBag.Usernames = Employ;

            return View(db.timesheets
                .OrderByDescending(p => p.CreatedOn)
                .Where(c => Depart.Contains(c.Department) &&
                            (option == null || option == "" || c.Owner == (option)) && // If option is not provided, don't filter by owner
                            (Duration == null || Duration == "" || (c.From_Date >= startRange && c.From_Date <= endRange)))
                .ToList()
                .ToPagedList(page ?? 1, 11));
        }



        // Helper function to get the week from a date



        private Direct_Activities_Context DA = new Direct_Activities_Context();
        private Indirect_Activities_Context IA = new Indirect_Activities_Context();
        public ActionResult Directtasks(int id, int? page,string user)
        {
            var data =DA.direct_Activities.Where(c => c.User == user && c.WeekNo == id).ToList().ToPagedList(page ?? 1, 11).ToList();
            ViewBag.user = user;
            ViewBag.weeNo = id;
            var TotalTime = DA.direct_Activities.Where(c => c.User == user && c.WeekNo == id).Select(c=>c.Hours).DefaultIfEmpty(0).Sum(); ;
            ViewBag.TotalTime = TotalTime;
            var TotalCharge = DA.direct_Activities.Where(c => c.User == user && c.WeekNo == id).Select(c => c.Charge).DefaultIfEmpty(0).Sum(); ;
            ViewBag.TotalCharge=TotalCharge;
            return PartialView("Directtasks", data);
        }
        public ActionResult InDirecttasks(int id, int? page, string user)
        {
            var data = IA.indirect_Activities.Where(c => c.User == user && c.WeekNo == id).ToList().ToPagedList(page ?? 1, 11).ToList();

            ViewBag.user = user;
            ViewBag.weeNo = id;
            return PartialView("InDirecttasks", data);
        }












        private Userstable ddb = new Userstable();
        public ActionResult ApproveTimesheet(int id ,string user,DateTime From_Date ,DateTime End_Date)
        {
           var DirectActivitiestasks = DA.direct_Activities.ToList();
           var IndirectActivitiestasks =IA.indirect_Activities.ToList();
            
            foreach (var task in DirectActivitiestasks.Where(t=>t.Day_Date >= From_Date && t.Day_Date <= End_Date && t.User==user))
            {
                // Update the status of the direct activity
                task.Approved = true;
                DA.SaveChanges();
            }

            foreach (var task in IndirectActivitiestasks.Where(t => t.Day_Date >= From_Date && t.Day_Date <= End_Date && t.User == user))
            {
                // Update the status of the indirect activity
                task.Approved = true;
                IA.SaveChanges();
            }


            Timesheet check = db.timesheets.Find(id);
            if (check != null)
            {
                check.Locked = true;
                check.Status = 7;
                db.SaveChanges();
            }

            var Times = db.timesheets.Where(c => c.Id == id).FirstOrDefault();
            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Approved Successfully  Month : " + Times.CreatedOn.ToString("MMMM") + "  Duration : " + Times.From_Date.ToString("dd-MMMM-yyyy")  + " To " + Times.End_Date.ToString("dd-MMMM-yyyy");
            string message = daytime + " ," + user
            + "\n" + "Your timesheet  Month: " + Times.CreatedOn.ToString("MMMM") + "Duration: " + Times.From_Date.ToString("dd - MMMM - yyyy")  + " To " + Times.End_Date.ToString("dd - MMMM - yyyy") +  " has been Approved successfully  "
            + "\n" + "thank you "
            + "\n" + "Regards , HR-Team ";
            var usser = ddb.AspNetUsers.Where(t => t.UserName == user).FirstOrDefault();
            PushEmail(usser.Email, Subject, message, DateTime.Now);
            PushSms(usser.PhoneNumber, message, DateTime.Now);

            return RedirectToAction("All", "Timesheet");
        }
        public ActionResult Decline(int id, string user)
        {
            

            Timesheet check = db.timesheets.Find(id);
            if (check != null)
            {
                check.Locked = false;
                check.Status = 99;
                db.SaveChanges();
            }

            var Times = db.timesheets.Where(c => c.Id == id).FirstOrDefault();
            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Declined   Month : " + Times.CreatedOn.ToString("MMMM") + "Duration : " + Times.From_Date.ToString("dd-MMMM-yyyy") + " To " + Times.End_Date.ToString("dd-MMMM-yyyy");
            string message = daytime + " ," + user
            + "\n" + "Your timesheet  Month: " + Times.CreatedOn.ToString("MMMM") + "Duration: " + Times.From_Date.ToString("dd - MMMM - yyyy") + " To " + Times.End_Date.ToString("dd - MMMM - yyyy") + " has been  declined  "
            + "\n" + "kindly note  your timesheet has been re-openned ,this allows you to make the needed ajustments to your timesheet "
            + "\n" + "Regards , HR-Team ";
            var usser = ddb.AspNetUsers.Where(t => t.UserName == user).FirstOrDefault();
            PushEmail(usser.Email, Subject, message, DateTime.Now);
            PushSms(usser.PhoneNumber, message, DateTime.Now);

            return RedirectToAction("All", "Timesheet");
        }






        public ActionResult DataView(DateTime selectedDate,string user)
        {
            ViewBag.SelectedDate = selectedDate;
            ViewBag.user = user;

            DateTime endDate = selectedDate.Date.AddDays(1).AddMinutes(-1);
            DateTime endDate2 = selectedDate.Date.AddDays(1).AddMinutes(-1);
            var data = IA.indirect_Activities.Where(c => c.User == user && c.Day_Date >= selectedDate && c.Day_Date < endDate).ToList();
            ViewBag.IndirectActivities=data;
            var data2 = DA.direct_Activities.Where(c => c.User == user && c.Day_Date >= selectedDate && c.Day_Date < endDate2).ToList();
            ViewBag.direct_Activities = data2;

            var returnUrl = Url.Action("DataView", "Timesheet");

            return View("DataView");
        }



        public ActionResult BreakDown(DateTime FromDate, DateTime EndDate, string user,int id)
        {
            var Times = db.timesheets.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.timesheet = Times;

            Employee_Context EC = new Employee_Context();
            var employee = EC.employees.Where(c => c.Username == Times.Owner).FirstOrDefault();

            ViewBag.Employee = employee;

            ViewBag.FromDate = FromDate;
            ViewBag.EndDate = EndDate;
            ViewBag.user = user;

            var data = IA.indirect_Activities.OrderBy(c => c.Day_Date).Where(c => c.User == user && c.Day_Date >= FromDate && c.Day_Date < EndDate).ToList();
            ViewBag.IndirectActivities = data;
            var data2 = DA.direct_Activities.OrderBy(c=>c.Day_Date).Where(c => c.User == user && c.Day_Date >= FromDate && c.Day_Date < EndDate).ToList();
            ViewBag.direct_Activities = data2;

            var returnUrl = Url.Action("BreakDown", "Timesheet");

            Sheet_comments_context sc = new Sheet_comments_context();
            var approvals =sc.sheet_Comments.Where(c=>c.Timesheeet_Id == id).ToList();
            ViewBag.approvals = approvals;

            return View("BreakDown");
        }

















        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool TimesheetExists(DateTime from_Date , string employeeUsername)
        {
            
            return db.timesheets.Any(c => c.From_Date == from_Date && c.Owner == employeeUsername);
        }



        public ActionResult Re_submit(int id)
        {

            var Sheet = db.timesheets.Find(id);

            if (Sheet != null)
            {
                Sheet.Locked = true;
                Sheet.Status = 0;
                db.SaveChanges();
            }
            string Subject = "Timesheets Re-Submitted  ";
            string message = "Hello " + Sheet.Owner
                + "\n" + "Kindly note your  timesheet. has been resubmitted and its now locked "
                + "\n" + "Timesheet Details "
                + "\n" + "  _   _   _  _ "
                + "\n" + "From Date " + Sheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Sheet.End_Date.ToString("dd - MMMM - yyyy") + " "
                + "\n" + "Project Time " + Sheet.Direct_Hours
                + "\n" + "Non Project Time " + Sheet.InDirect_Hours
                + "\n" + "Total Time  " + Sheet.Tt
                + "\n" + "Regards, HR-Team ";

            Employee_Context EM = new Employee_Context();
            var Empl = EM.employees.Where(c => c.Username == Sheet.Owner).FirstOrDefault();
            PushEmail(Empl.Email, Subject, message, DateTime.Now);
            TempData["msg"] = "Timesheet Re-submitted successfully  -   Staff : " + Sheet.Owner + " From Date " + Sheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Sheet.End_Date.ToString("dd - MMMM - yyyy");
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

        









       public void InsertTimesheet()
        {
            DateTime currentDate = DateTime.Now.Date;
            List<Timesheet> timesheetsToUpdate = db.timesheets.Where(c => c.CreatedOn <= currentDate).ToList();

            foreach (var timesheet in timesheetsToUpdate)
            {
                timesheet.Locked = true;
            }

            db.SaveChanges();

            
        }

        
        private string connectionString = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
        public void PushEmail(string Recipient, string Subject, string Body, DateTime CreatedOn)
        {
            string query = "INSERT INTO OutgoingEmails (Recipient,Subject,Body,Status,CreatedOn,Trials,Response) VALUES " +
            "                                          (@Recipient,@Subject, @Body,0,@CreatedOn,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Recipient", Recipient);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.Parameters.AddWithValue("@Body", Body);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }
        public void PushSms(string Recipient, string Subject, DateTime CreatedOn)
        {
            string query = "INSERT INTO Outgoingsms (MessageText,IsSent,CreatedOn,RecipientNumber,Trials,Response) VALUES " +
            "                                          (@MessageText,@IsSent,@CreatedOn,@RecipientNumber,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@MessageText", Subject);
                    command.Parameters.AddWithValue("@IsSent", 0);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@RecipientNumber", Recipient);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }
    }




}

