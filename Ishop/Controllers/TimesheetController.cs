using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using PagedList;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using Ishop.Models;
using IShop.Core.Interface;
using Microsoft.AspNet.SignalR.Hosting;
using System.Data.SqlClient;
using System.Configuration;

namespace Ishop
{
    
    public class TimesheetController : Controller
    {
        private Userstable dbb = new Userstable();
        private Timesheet_Context db = new Timesheet_Context();

        [Authorize]
        public ActionResult Index(int? page)
        {

            var Userinfo = dbb.AspNetUsers.Where(a => a.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.userinfo = Userinfo;
            return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Owner == User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));


        }
        public ActionResult Approval(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Owner.StartsWith(search) || c.Owner == search ).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        public ActionResult Details(int? id)
        {

            Activities_Context AA = new Activities_Context();
            var Act = AA.activities.Where(a => a.TimesheetId == id).ToList() ;
            ViewBag.Activities = Act;
            


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }





        public ActionResult Re_Open(int Id)
        {
            var Owner = db.timesheets.Where(t => t.Id == Id).FirstOrDefault();
            var U = UA.AspNetUsers.Where(t => t.UserName == Owner.Owner).FirstOrDefault();

            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Re-Opened Week No " + Owner.Weekid;
            string message = daytime + " ," + Owner.Owner
            + "\n" + "Your timesheet has been re-opened by management "
            + "\n" + "kindly login and fill in your timesheet details "
            + "\n" + "Regards , HR-Team ";

            PushEmail(U.Email, Subject, message, DateTime.Now);
            PushSms(U.PhoneNumber, message, DateTime.Now);
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 0;
                db.SaveChanges();
            }
            TempData["msg"] = "✔  Timesheet Re-Openned successfully - Employee Notified";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }





        private Userstable UA = new Userstable();
        [Authorize]
        public ActionResult Submit(int Id)
        {
            var Owner = db.timesheets.Where(t => t.Id == Id).FirstOrDefault();
            var U = UA.AspNetUsers.Where(t => t.UserName == Owner.Owner).FirstOrDefault();
            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Submitted Successfully Week No " + Owner.Weekid;
            string message = daytime + " ," + Owner.Owner
            + "\n" + "Your timesheet has been submitted successfully  "
            + "\n" + "kindly note management team will notify you on the approvals via mail soon."
            + "\n" + "thank you "
            + "\n" + "Regards , HR-Team ";

            PushEmail(U.Email, Subject, message, DateTime.Now);
            PushSms(U.PhoneNumber, message, DateTime.Now);
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 1;
                db.SaveChanges();
            }

            TempData["msg"] = "✔  Timesheet Submitted successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }


        public ActionResult Approve(int Id)
        {
            var Owner = db.timesheets.Where(t => t.Id == Id).FirstOrDefault();
            var U = UA.AspNetUsers.Where(t => t.UserName == Owner.Owner).FirstOrDefault();
            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Approved Successfully Week No " + Owner.Weekid;
            string message = daytime + " ," + Owner.Owner
            + "\n" + "Your timesheet has been Approved successfully  "
            + "\n" + "thank you "
            + "\n" + "Regards , HR-Team ";

            PushEmail(U.Email, Subject, message, DateTime.Now);
            PushSms(U.PhoneNumber, message, DateTime.Now);
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 2;
                db.SaveChanges();
            }

            TempData["msg"] = "✔  Timesheet Approved successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
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

        public bool TimesheetExists(int weekNumber, string employeeUsername)
        {
            return db.timesheets.Any(c => c.Weekid == weekNumber && c.Owner == employeeUsername);
        }
       

        public void InsertTimesheet()
        {
            DateTime currentDate = DateTime.Now;
            int daysInWeek = 7;

            // Calculate the day of the week the month starts
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int daysPassed = (int)firstDayOfMonth.DayOfWeek;

            // Adjust daysPassed if the first day of the month is not Sunday (0)
            daysPassed = (daysPassed == 0) ? 0 : 7 - daysPassed;

            // Calculate the week number
            int weekNumber = (currentDate.Day + daysPassed - 1) / daysInWeek + 1;

            string weekInfo = $"{weekNumber}{DateTime.Now.Month}{DateTime.Now.Year}";
            string joinedStringConcat = string.Concat(weekInfo);
            int.TryParse(joinedStringConcat, out int WeekNo);
              



            Employee_Context Emp = new Employee_Context();
                List<Employee> employees = Emp.employees.ToList();

                foreach (var employee in employees)
                {
                    // Check if the timesheet already exists for the current week and employee
                    if (!TimesheetExists(WeekNo, employee.Username))
                    {
                        // Create a new Timesheet record
                        Timesheet timesheet = new Timesheet
                        {
                            CreatedOn = DateTime.Now,
                            Owner = employee.Username,
                            Weekid = WeekNo,
                            // Set other day-specific values here
                            Sun = 0,
                            Mon = 0,
                            Tue = 0,
                            Wen = 0,
                            Thur = 0,
                            Fri = 0,
                            Sat = 0,
                            Tt = 0,
                            Status = 0
                        };


                    var timesheetsToUpdate = db.timesheets.Where(c => c.Weekid < WeekNo).ToList();

                    foreach (var check in timesheetsToUpdate)
                    {
                        check.Status = 1;
                        db.SaveChanges();
                    }
                    // Insert the record into the "timesheet" table
                    db.timesheets.Add(timesheet);
                        db.SaveChanges();
                   
                    }
                    else
                {
                    
                }

            }
            
            
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

