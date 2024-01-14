using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using IShop.Core.Interface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Http.Results;
using System.Web.Mvc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Ishop
{

    public class TimesheetController : Controller
    {
        private Userstable dbb = new Userstable();
        private Timesheet_Context db = new Timesheet_Context();

        [Authorize]
        public ActionResult Index()
        {


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


        [Authorize]
        public ActionResult Mine (string searchBy, string search, int? page)
        {
              
          return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Owner == User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));

                
           
        }
















        [Authorize]
        public ActionResult Approval( int? page, string option, string startDate, string endDate)
        {
            var Employ = db.timesheets.Select(t => t.Owner).Distinct().ToList();
            ViewBag.Usernames = Employ;


            DepartmentContext DD = new DepartmentContext();
            var Depart = DD.departments.Where(D => D.Manager == User.Identity.Name).Select(d => d.DprtName).FirstOrDefault();

            if (!(option == null) && (!(option == "")))
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Owner.StartsWith(option) || c.Owner == option && c.Department ==Depart).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (option == "")
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).Where(c=>c.Department == Depart).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Department == Depart).ToList().ToPagedList(page ?? 1, 11));
            }
            

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
        public ActionResult ApproveTimesheet(int Id,string user)
        {
           var DirectActivitiestasks = DA.direct_Activities.ToList();
           var IndirectActivitiestasks =IA.indirect_Activities.ToList();
            

            // Assuming you have a model with a property named "Id" and "User" in both direct and indirect activities
            foreach (var task in DirectActivitiestasks.Where(t => t.WeekNo == Id && t.User == user))
            {
                // Update the status of the direct activity
                task.Approved = true;
                DA.SaveChanges();
            }

            foreach (var task in IndirectActivitiestasks.Where(t => t.WeekNo == Id && t.User == user))
            {
                // Update the status of the indirect activity
                task.Approved = true;
                IA.SaveChanges();
            }

            // Save changes to the database
            int currentYear = DateTime.Now.Year;
            int weekNumber = Id;
            DateTime jan1 = new DateTime(currentYear, 1, 1);
            DateTime specifiedWeekStart = jan1.AddDays((weekNumber - 1) * 7 - (int)jan1.DayOfWeek + (int)DayOfWeek.Monday);
            string monthName = specifiedWeekStart.ToString("MMMM", CultureInfo.InvariantCulture);
            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Approved Successfully Week No " + Id;
            string message = daytime + " ," + user
            + "\n" + "Your timesheet for Week Number " + Id +" of month " + monthName + " has been Approved successfully  "
            + "\n" + "thank you "
            + "\n" + "Regards , HR-Team ";
            var usser = ddb.AspNetUsers.Where(t => t.UserName == user).FirstOrDefault();
            PushEmail(usser.Email, Subject, message, DateTime.Now);
            PushSms(usser.PhoneNumber, message, DateTime.Now);
            TempData["msg"] = "Timesheet apporved successfully - WeekNo" + Id  + "-Staff  "+ user;
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

        public bool TimesheetExists(DateTime from_Date , string employeeUsername)
        {
            
            return db.timesheets.Any(c => c.From_Date == from_Date && c.Owner == employeeUsername);
        }

       
        public void InsertTimesheet()
        {
            DateTime currentDate = DateTime.Now;

            // Calculate the day of the week the month starts
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int daysPassed = (int)firstDayOfMonth.DayOfWeek;

            // Adjust daysPassed if the first day of the month is not Sunday (0)
            daysPassed = (daysPassed == 0) ? 0 : 7 - daysPassed;

            // Calculate the week number based on the starting day of the month
            int currentWeekNumber = GetCurrentWeekNumber(DateTime.Today);
            int weekNumber = currentWeekNumber;

            string weekInfo = $"{weekNumber:D2}{currentDate.Month:D2}{currentDate.Year}";
            string joinedStringConcat = string.Concat(weekInfo);
            int.TryParse(joinedStringConcat, out int WeekNo);




            Employee_Context Emp = new Employee_Context();
            List<Employee> employees = Emp.employees.ToList();
            DateTime currentDatee = DateTime.Now.Date;
            DateTime from_Date = currentDatee;
            DateTime end_Date = currentDate.AddDays((DayOfWeek.Saturday - currentDate.DayOfWeek + 7) % 7);
            foreach (var employee in employees)
            {
                // Check if the timesheet already exists for the current week and employee
                if (!TimesheetExists(from_Date, employee.Username))
                {
                    // Create a new Timesheet record
                    Timesheet timesheet = new Timesheet
                    {
                        CreatedOn = DateTime.Now,
                        Department =employee.DprtName,
                        From_Date= from_Date,
                        End_Date= end_Date,
                        Owner = employee.Username,
                        Tt = 0,
                        Direct_Hours=0,
                        InDirect_Hours=0,
                        Status = 0
                    };

                    db.timesheets.Add(timesheet);
                    db.SaveChanges();

                }
                else
                {

                }

            }


        }
        private int GetCurrentWeekNumber(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
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

